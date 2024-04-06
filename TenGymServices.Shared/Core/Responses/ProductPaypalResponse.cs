using TenGymServices.Shared.Core.Interfaces;

namespace TenGymServices.Shared.Core.Responses
{
    public class ProductPaypalResponse : IId
    {
        public string Id { get; set; }
        public string name { get; set; }
        public DateTime? create_time { get; set; }
    }
}