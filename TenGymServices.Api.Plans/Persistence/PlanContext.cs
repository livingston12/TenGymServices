using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Core.Entities;

namespace TenGymServices.Api.Plans.Persistence
{
    public class PlanContext : DbContext
    {
        public PlanContext(DbContextOptions options) : base(options) {}

        public DbSet<PlanEntity> Plans { get; set; }
        public DbSet<BillingCycleEntity> BillingCycles { get; set; }
        public DbSet<FixedPriceEntity> FixedPrices { get; set; }
        public DbSet<FrequencyEntity> Frequencies { get; set; }
        public DbSet<PaymentPreferenceEntity> PaymentPreferences { get; set; }
        public DbSet<PricingSchemeEntity> PricingSchemes { get; set; }
        public DbSet<SetupFeeEntity> SetupFees { get; set; }
        public DbSet<TaxEntity> Taxes { get; set; }
    }
}