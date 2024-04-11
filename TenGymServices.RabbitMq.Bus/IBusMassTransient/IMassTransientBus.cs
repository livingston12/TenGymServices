using MassTransit;

namespace TenGymServices.RabbitMq.Bus.IBusMassTransient
{
    public interface IMassTransientBus
    {
        public void Publish<TEvent>(TEvent message);
    }
}