using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Products.RabbitMq.Queues
{
    public class ProductEventQuee : Event
    {
        public string? PaypalId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
        public string? HomeUrl { get; set; }
    }
}