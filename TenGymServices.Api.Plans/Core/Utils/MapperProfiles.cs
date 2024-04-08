using AutoMapper;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.EventQuee;

namespace TenGymServices.Api.Plans.Core.Utils
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {
      CreateMap<CreatePlanCommand, PlanEventQuee>();

      CreateMap<PlanDto, PatchPlanDto>()
          .ForMember(request => request.Tax, opt => opt.MapFrom((src, dest) => src.Taxes));
      CreateMap<PatchPlanCommand, PatchPlanQuee>();

      CreateMap<PlanEntity, PlanDto>();
      CreateMap<PlanEventQuee, PlanEntity>();
      CreateMap<BillingCyclesDto, BillingCycleEntity>();
      CreateMap<SetupFeeDto, SetupFeeEntity>();
      CreateMap<FixedPriceDto, FixedPriceEntity>();
      CreateMap<FrequencyDto, FrequencyEntity>();
      CreateMap<PaymentPreferencesDto, PaymentPreferenceEntity>();
      CreateMap<PricingSchemeDto, PricingSchemeEntity>();
      CreateMap<TaxDto, TaxEntity>();
    }
  }
}