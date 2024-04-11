using TenGymServices.Api.Products.Core.Dtos;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Products.RabbitMq.Queues
{
    public class PatchProductQuee : Event
    {
        public int ProductId { get; set; }
        public ProductPatchDto PatchProduct { get; set; }
    }
}