using TenGymServices.Api.Products.Core.Interfaces;
using TenGymServices.Shared.Core.Dtos;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Requests;
using TenGymServices.Shared.Core.Responses;

namespace TenGymServices.Api.Products.Aplication.ExternalServices
{
    public class PaypalService : IPaypalProductService
    {
        public readonly IHttpClientFactory _httpClientFactoty;

        public PaypalService(IHttpClientFactory httpClient)
        {
            _httpClientFactoty = httpClient;
        }

        public async Task<(bool hasEerror, string Id, string MessageError)> PostAsync(ProductPaypalRequest request, string method, HttpMethod httpMethod = null)
        {
            var httpClient = _httpClientFactoty.CreateClient("PaypalClient");
            
            var response = await httpClient.PostGenericAsync(request, method);
            var content = await response.Content.ReadAsStringAsync();

            return response.GenerateMessage<ProductPaypalResponse>(content);
        }

        public Task<GeneralDto<ProductPaypalDto>> GetProductId(int ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductPaypalDto>> GetProducts(PaginationPaypalRequest pagination)
        {
            throw new NotImplementedException();
        }
    }
}