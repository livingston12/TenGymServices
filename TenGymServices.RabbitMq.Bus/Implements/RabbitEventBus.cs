using System.Text;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.Commands;
using TenGymServices.RabbitMq.Bus.Events;
using Microsoft.Extensions.Logging;

namespace TenGymServices.RabbitMq.Bus.Implements
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        public string HostName { get; set; }
        public string Exchange { get; set; }
        private readonly ILogger<RabbitEventBus> _logger;

        public RabbitEventBus(IMediator mediator, ILogger<RabbitEventBus> logger)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            HostName ??= "localhost";
            Exchange ??= "default";
            _logger = logger;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            using (var con = factory.CreateConnection())
            using (var channel = con.CreateModel())
            {
                channel.ExchangeDeclare(exchange: Exchange, type: ExchangeType.Fanout);
                var eventName = @event.GetType().Name;
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(Exchange, string.Empty, null, body);
                _logger.LogInformation($"Published event {eventName} to exchange {Exchange}");
            }
        }

        public Task SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return _mediator.Send(command);
        }

        public void Suscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = typeof(TEvent).Name;
            _logger.LogInformation($"Subscribing to event {eventName}");
            var eventHandlerType = typeof(TEventHandler);

            if (!_eventTypes.Contains(typeof(TEvent)))
            {
                _eventTypes.Add(typeof(TEvent));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(x => x == eventHandlerType))
            {
                throw new ArgumentException($"The handler {eventHandlerType.Name} was already registered for event {eventName}");
            }

            _handlers[eventName].Add(eventHandlerType);

            var factory = new ConnectionFactory()
            {
                HostName = HostName,
                DispatchConsumersAsync = true
            };

            using (var con = factory.CreateConnection())
            using (var channel = con.CreateModel())
            {
                channel.ExchangeDeclare(exchange: Exchange, type: ExchangeType.Fanout);
                channel.QueueDeclare(eventName, true, false, false, null);
                channel.QueueBind(queue: eventName, exchange: Exchange, routingKey: "");
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    await consumerDelegate(eventName, model, ea);
                };

                channel.BasicConsume(eventName, true, consumer);
                _logger.LogInformation($"Subscribed to event {eventName} on exchange {Exchange}");
            }
        }

        private async Task consumerDelegate(string eventName, object model, BasicDeliverEventArgs ea)
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
            var eventObject = JsonConvert.DeserializeObject(message, eventType);
            foreach (var handlerType in _handlers[eventName])
            {
                var handler = Activator.CreateInstance(handlerType);
                var handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod != null)
                {
                    await (Task)handleMethod.Invoke(handler, new[] { eventObject });
                }
            }
        }
    }
}