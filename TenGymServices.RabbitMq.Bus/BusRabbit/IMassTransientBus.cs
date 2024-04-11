using MassTransit;

namespace TenGymServices.RabbitMq.Bus.BusRabbit
{
    public interface IMassTransientBus
    {
        public void Publish<TEvent>(TEvent message);
    }
}