using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Shared.Core.Interfaces;

namespace TenGymServices.Api.Plans.Core.Interfaces
{
    public interface IPaypalPlansService<TRequest> : IPaypalService<TRequest>
        where TRequest : class
    {
        public Task<List<PlanDto>> GetAllPlans(string productPaypalId);
        public Task<PlanDto> GetByIdPlan(string productPaypalId);
    }
}