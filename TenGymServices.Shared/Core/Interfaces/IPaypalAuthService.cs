namespace TenGymServices.Shared.Core.Interfaces
{
    public interface IPaypalAuthService
    {
        public Task<string> GenerateToken(HttpClient client, string clientId, string secretId);
    }
}