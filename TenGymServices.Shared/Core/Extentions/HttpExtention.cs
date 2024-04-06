

using System.Net;
using System.Text;
using Newtonsoft.Json;
using TenGymServices.Shared.Core.Dtos;
using TenGymServices.Shared.Core.Interfaces;

namespace TenGymServices.Shared.Core.Extentions
{
    public static class HttpExtention
    {
        public static async Task<HttpResponseMessage> PostGenericAsync<TRequest>(this HttpClient httpClient,
            TRequest request, string method)
        {
            string dataJson = JsonConvert.SerializeObject(request);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, method)
            {
                Content = new StringContent(dataJson, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(requestMessage);

            return response;
        }

        public static (bool hasEerror, string ProductId, string MessageError) GenerateMessage<TRequest>(this HttpResponseMessage response, string content)
            where TRequest : IId
        {
            (bool hasEerror, string ProductId, string MessageError) result = (true, string.Empty, string.Empty);
            if (response.IsSuccessStatusCode)
            {
                var responseMap = JsonConvert.DeserializeObject<TRequest>(content);
                return (false, responseMap.Id, string.Empty);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.MessageError = "You are unauthorized please valide the credentials";
            }
            var error = JsonConvert.DeserializeObject<ErrorMessage>(content);
            result.MessageError = error?.Message ?? string.Empty;

            return result;
        }
    }
}