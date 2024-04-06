using TenGymServices.Shared.Core.Dtos;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Core.Requests;

namespace TenGymServices.Api.Products.Core.Interfaces
{
    public interface IPaypalProductService : IPaypalService<ProductPaypalRequest>
    {
        public Task<List<ProductPaypalDto>> GetProducts(PaginationPaypalRequest pagination);
        public Task<GeneralDto<ProductPaypalDto>> GetProductId(int ProductId);
    }
}