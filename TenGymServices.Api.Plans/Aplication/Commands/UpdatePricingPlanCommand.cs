using MediatR;
using Newtonsoft.Json;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public class UpdatePricingPlanCommand : UpdatePricingPlanDto, IRequest, IPlanId
    {
        public int PlanId {get; set;}
    }
}