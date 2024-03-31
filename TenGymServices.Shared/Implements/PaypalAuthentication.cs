using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TenGymServices.Shared.Core.Dtos;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Core.Responses;

namespace TenGymServices.Shared.Implements
{
    public class PaypalAuthService : IPaypalAuthService
    {
        private string? _accessToken { get; set; }
        private DateTime _accessTokenExpiration { get; set; }
        private readonly IHttpClientFactory _clientFactory;

        public PaypalAuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> GenerateToken(HttpClient client, string clientId, string secretId)
        {
           
            if (DateTime.UtcNow >= _accessTokenExpiration)
            {
                var response = await GenerateNewToken(client, clientId, secretId);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var tokenInfo = JsonConvert.DeserializeObject<TokenPaypalResponse>(content);
                    _accessToken = tokenInfo.access_token;
                    _accessTokenExpiration = DateTime.UtcNow.AddSeconds(tokenInfo.expires_in);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _accessToken = string.Empty;
                }
            }

            return _accessToken;
        }

        private async Task<HttpResponseMessage> GenerateNewToken(HttpClient client, string clientId, string secretId)
        {
            string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{secretId}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            var collection = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("ignoreCache", "true"),
                new("return_authn_schemes", "true"),
                new("return_client_metadata", "true"),
                new("return_unconsented_scopes", "true")
            };
            var content = new FormUrlEncodedContent(collection);
            requestMessage.Content = content;
            return await client.SendAsync(requestMessage);
        }
    }
}