using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.RabbitMq.Queues
{
    public class PlanEventQuee : PlanDto
    {
        public string PaypalId { get; set; }
    }
}