namespace TenGymServices.Api.Products.Models.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? PaypalId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? HomeUrl { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
    }
}