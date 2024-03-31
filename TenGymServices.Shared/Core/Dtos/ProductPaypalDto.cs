namespace TenGymServices.Shared.Core.Dtos
{
    public class ProductPaypalDto
    {
        public string? PaypalId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? ImageUrl { get; set; }
        public string? HomeUrl { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}