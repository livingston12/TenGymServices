using AutoMapper;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.RabbitMq.Queues;

namespace TenGymServices.Api.Plans.Core.Utils
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {

      CreateMap<PlanDto, PatchPlanDto>()
        .ForMember(request => request.Tax, opt => opt.MapFrom((src, dest) => src.Taxes))
        .ReverseMap();
      CreateMap<UpdatePricingPlanCommand, UpdatePricingPlanQuee>()
        .ForMember(request => request.ListPricingPlans, opt => opt.MapFrom((src, dest) => src.PricingSchemes));
      CreateMap<UpdatePricingSchemePlanDto, PricingSchemeEntity>()
        .ForMember(request => request.FixedPrice, opt => opt.MapFrom((src, dest) => src.PricingScheme.FixedPrice));

      CreateMap<CreatePlanCommand, PlanEventQuee>();
      CreateMap<PatchPlanQuee, PatchPlanCommand>();
      CreateMap<PlanEntity, PlanDto>();
      CreateMap<PlanEventQuee, PlanEntity>();
      CreateMap<BillingCyclesDto, BillingCycleEntity>();
      CreateMap<SetupFeeDto, SetupFeeEntity>();
      CreateMap<FixedPriceDto, FixedPriceEntity>();
      CreateMap<FrequencyDto, FrequencyEntity>();
      CreateMap<PaymentPreferencesDto, PaymentPreferenceEntity>();
      CreateMap<PricingSchemeDto, PricingSchemeEntity>();
      CreateMap<TaxDto, TaxEntity>();
      CreateMap<PatchPlanDto, PlanEntity>();
      


    }

    private FixedPriceEntity SetFixedPricing(FixedPriceDto src, FixedPriceEntity dest)
    {
      FixedPriceEntity schemeEntity = new FixedPriceEntity();

      //schemeEntity.Value = src.PricingScheme.FixedPrice.Value;
      //schemeEntity.CurrencyCode = src.PricingScheme.FixedPrice.CurrencyCode;
      //schemeEntity.FixedPriceId = 
      return null;
     //return dest.FixedPrice;
    }
  }
}