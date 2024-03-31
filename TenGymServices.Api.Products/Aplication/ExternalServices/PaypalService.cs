using System.Net;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using TenGymServices.Api.Products.Services;
using TenGymServices.Shared.Core.Dtos;
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

        public async Task<(bool hasEerror, string ProductId, string MessageError)> CreateProduct(ProductPaypalRequest request)
        {
            var httpClient = _httpClientFactoty.CreateClient("PaypalClient");

            string dataJson = JsonConvert.SerializeObject(request);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v1/catalogs/products/")
            {
                Content = new StringContent(dataJson, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            return GenerateMessage(response, content);
        }

        private (bool hasEerror, string ProductId, string MessageError) GenerateMessage(HttpResponseMessage response, string content)
        {
            (bool hasEerror, string ProductId, string MessageError) result = (true, string.Empty, string.Empty);
            if (response.IsSuccessStatusCode)
            {
                var responseMap = JsonConvert.DeserializeObject<ProductPaypalResponse>(content);
                return (false, responseMap.id, string.Empty);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.MessageError = "You are unauthorized please valide the credentials";
            }

            var error = JsonConvert.DeserializeObject<ErrorMessage>(content);
            result.MessageError = error?.Message ?? string.Empty;

            return result;
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