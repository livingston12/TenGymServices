using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.EventQuee
{
    public class PlanEventQuee : PlanDto
    {
        public string PaypalId { get; set; }
    }
}