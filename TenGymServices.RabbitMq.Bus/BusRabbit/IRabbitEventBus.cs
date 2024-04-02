using TenGymServices.RabbitMq.Bus.Commands;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.RabbitMq.Bus.BusRabbit
{
    public interface IRabbitEventBus : IConfigurationRabbitmq
    {
        Task SendCommand<TCommand>(TCommand command) where TCommand : Command;
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Suscribe<TEvent, TEventHandler>() where TEvent : Event
                                               where TEventHandler : IEventHandler<TEvent>;
     }

    
}