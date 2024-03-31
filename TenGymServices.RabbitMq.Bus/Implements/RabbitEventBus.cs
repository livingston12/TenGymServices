using System.Text;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.Commands;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.RabbitMq.Bus.Implements;

public class RabbitEventBus : IRabbitEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    public string _hostName { get; set; }

    public RabbitEventBus(IMediator mediator)
    {
        _mediator = mediator;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _hostName ??= "localhost";
    }


    // Publish or send the Queue
    public void Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        var factory = new ConnectionFactory() { HostName = _hostName };
        using (var con = factory.CreateConnection())
        using (var channel = con.CreateModel())
        {
            var eventName = @event.GetType().Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            // Publish the queue
            channel.BasicPublish("", eventName, null, body);
        }
    }

    public Task SendCommand<TCommand>(TCommand command) where TCommand : Command
    {
        return _mediator.Send(command);
    }

    // Suscribe or consumer the Queue
    public void Suscribe<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>
    {
        // The name of the queue is the type of the objects
        var eventName = typeof(TEvent).Name;
        var EventhandlerType = typeof(TEventHandler);
        // Create news event types
        if (!_eventTypes.Contains(typeof(TEvent)))
        {
            _eventTypes.Add(typeof(TEvent));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        // if the event was register before thow a exeption 
        if (_handlers[eventName].Any(x => x.GetType() == EventhandlerType))
        {
            throw new ArgumentException($"The handler {EventhandlerType.Name} was inserted before by {eventName}");
        }

        _handlers[eventName].Add(EventhandlerType);

        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            DispatchConsumersAsync = true
        };

        var con = factory.CreateConnection();
        var channel = con.CreateModel();

        channel.QueueDeclare(eventName, false, false, false, null);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += ConsumerDelegate;
        channel.BasicConsume(eventName, true, consumer);
    }

    // Read the messages from queues
    private async Task ConsumerDelegate(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.ToArray());
        
        try
        {
            if (_handlers.ContainsKey(eventName)) 
            {
                var subscriptions = _handlers[eventName];

                foreach (var scb in subscriptions)
                {
                    var handler = Activator.CreateInstance(scb);
                    if (handler == null) continue;
                   
                    var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                    var eventDS = JsonConvert.DeserializeObject(message, eventType);

                    var concretType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    
                    await (Task)concretType
                            .GetMethod("Handle")
                            .Invoke(handler, new object[] {eventDS});
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}
