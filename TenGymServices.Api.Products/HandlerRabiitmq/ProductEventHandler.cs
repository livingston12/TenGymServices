using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.EventQuees;

namespace TenGymServices.Api.Products.HandlerRabiitmq
{
    public class ProductEventHandler : IEventHandler<ProductEventQuee>
    {
        public Task Handle(ProductEventQuee @event)
        {
            // Save product in database
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}