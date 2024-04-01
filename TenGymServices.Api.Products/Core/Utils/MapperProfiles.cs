
using AutoMapper;
using TenGymServices.Api.Products.Models.Dtos;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Models.Enums;
using TenGymServices.RabbitMq.Bus.EventQuees;
using TenGymServices.Shared.Core.Requests;
using static TenGymServices.Api.Products.Aplication.Post;

namespace TenGymServices.Api.Products.Core.Utils
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CreateTaskCommand, ProductPaypalRequest>()
                .ForMember(request => request.name, opt => opt.MapFrom((src, dest) => src.Name))
                .ForMember(request => request.type, opt => opt.MapFrom((src, dest) => TYPE.DIGITAL.ToString()))
                .ForMember(request => request.description, opt => opt.MapFrom((src, dest) => src.Description))
                .ForMember(request => request.category, opt => opt.MapFrom((src, dest) => CATEGORY.SCHOOLS_AND_COLLEGES))
                .ForMember(request => request.image_url, opt => opt.MapFrom((src, dest) => src.ImageUrl))
                .ForMember(request => request.home_url, opt => opt.MapFrom((src, dest) => src.HomeUrl));
            CreateMap<ProductPaypalRequest, ProductEventQuee>()
                .ForMember(request => request.PaypalId, opt => opt.MapFrom((src, dest) => src.paypal_id))
                .ForMember(request => request.HomeUrl, opt => opt.MapFrom((src, dest) => src.home_url))
                .ForMember(request => request.ImageUrl, opt => opt.MapFrom((src, dest) => src.image_url));

            CreateMap<CreateTaskCommand, ProductsEntity>();
            CreateMap<ProductsEntity, ProductDto>();

        }
    }
}