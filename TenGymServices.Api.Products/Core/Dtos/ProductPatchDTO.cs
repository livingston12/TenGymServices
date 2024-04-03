namespace TenGymServices.Api.Products.Core.Dtos
{
    public class ProductPatchDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string HomeUrl { get; set; }
    }
}