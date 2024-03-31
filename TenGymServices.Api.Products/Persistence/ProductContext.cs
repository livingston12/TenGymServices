using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Products.Models.Entities;

namespace TenGymServices.Api.Products.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) {}

        public DbSet<ProductsEntity> Products {get; set;}
    }
}