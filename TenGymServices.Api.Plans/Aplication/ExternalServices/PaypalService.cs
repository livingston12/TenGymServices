using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.Core.Reponses;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.ExternalServices
{
    public class PaypalService<TRequest> : IPaypalPlansService<TRequest>
        where TRequest : class
    {
        public readonly IHttpClientFactory _httpClientFactoty;

        public PaypalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactoty = httpClientFactory;
        }

        public async Task<(bool hasEerror, string Id, string MessageError)> PostAsync(TRequest request, string method, HttpMethod httpMethod = null)
        {
            HttpClient httpClient = _httpClientFactoty.CreateClient("PaypalClient");
            
            var response = await httpClient.PostGenericAsync(request, method, httpMethod);
            var content = await response.Content.ReadAsStringAsync();

            return response.GenerateMessage<PlanPaypalResponse>(content);
        }

        public Task<List<PlanDto>> GetAllPlans(string productPaypalId)
        {
            throw new NotImplementedException();
        }

        public Task<PlanDto> GetByIdPlan(string productPaypalId)
        {
            throw new NotImplementedException();
        }
    }
}