using TenGymServices.Shared.Core.Dtos;
using TenGymServices.Shared.Core.Requests;

namespace TenGymServices.Api.Products.Services
{
    public interface IPaypalProductService
    {
        public  Task<(bool hasEerror,string ProductId , string MessageError)> CreateProduct(ProductPaypalRequest request);
        public Task<List<ProductPaypalDto>> GetProducts(PaginationPaypalRequest pagination);
        public Task<GeneralDto<ProductPaypalDto>> GetProductId(int ProductId);
    }
}