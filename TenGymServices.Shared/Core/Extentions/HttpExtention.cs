

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
            TRequest request, string method, HttpMethod httpMethod = null)
            where  TRequest : class
        {
            var allowHttpMethods = new HttpMethod[] { HttpMethod.Post, HttpMethod.Patch, HttpMethod.Put };

            if (httpMethod == null)
            {
                httpMethod = HttpMethod.Post;
            }
            
            if (!allowHttpMethods.Contains(httpMethod))
            {
                request.ThrowHttpHandlerExeption("The Method is not allow for that operation", HttpStatusCode.BadRequest);
            }

            string dataJson = JsonConvert.SerializeObject(request);
            HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, method)
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
                return (false, responseMap?.Id ?? "", string.Empty);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.MessageError = "You are unauthorized please valide the credentials";
            }
            var error = JsonConvert.DeserializeObject<ErrorMessage>(content);
            result.MessageError = error?.Message ?? string.Empty;

            if (error?.Details != null)
            {
                result.MessageError = string.Join(Environment.NewLine, error.Details.Select(x => $"{x.Description}: {x.Field}"));
            }

            return result;
        }
    }
}