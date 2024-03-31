using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Products.Models.Enums;

namespace TenGymServices.Api.Products.Models.Entities
{
    public class ProductsEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The Field {0} cannot be empty")]
        public string? PaypalId { get; set; }

        [Required(ErrorMessage = "The Field {0} cannot be empty")]
        [StringLength(127)]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "The Field {0} cannot be empty")]
        [StringLength(24)]
        public string Type { get; set; } = TYPE.DIGITAL.ToString();
        
        [StringLength(256)]
        public string? Description { get; set; }
        
        [StringLength(256)]
        public string Category { get; set; } = CATEGORY.SCHOOLS_AND_COLLEGES.ToString();
        [StringLength(2000)]
        public string? ImageUrl { get; set; }
        [StringLength(2000)]
        public string? HomeUrl { get; set; }
    }
}