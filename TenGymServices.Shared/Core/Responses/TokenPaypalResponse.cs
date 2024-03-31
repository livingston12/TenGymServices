namespace TenGymServices.Shared.Core.Responses
{
    public class TokenPaypalResponse
    {
        public string? access_token { get; set; }
        public string? token_type { get; set; }
        public string? app_id { get; set; }
        public long expires_in { get; set; }
    }
}