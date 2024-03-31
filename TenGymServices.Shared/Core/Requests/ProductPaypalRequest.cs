namespace TenGymServices.Shared.Core.Requests;

public class ProductPaypalRequest
{
    public string paypal_id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public string image_url { get; set; }
    public string home_url { get; set; }
}
