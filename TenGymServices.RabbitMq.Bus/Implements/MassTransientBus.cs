using MassTransit;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;

namespace TenGymServices.RabbitMq.Bus.Implements
{
    public class MassTransientBus : IMassTransientBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransientBus(IPublishEndpoint publishEndpoint) 
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<TEvent>(TEvent message)
        {
            await _publishEndpoint.Publish(message);
        }
    }
}