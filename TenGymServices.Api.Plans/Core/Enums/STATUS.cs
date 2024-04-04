using System.Runtime.Serialization;

namespace TenGymServices.Api.Plans.Core.Enums;

public enum STATUS
{
    CREATED = 1, // The plan was created. You cannot create subscriptions for a plan in this state.
    [EnumMember(Value = "INACTIVE")]
    INACTIVE = 2, // The plan is inactive.
    //[EnumMember(Value = "ACTIVE")]
    ACTIVE = 3// The plan is active. You can only create subscriptions for a plan in this state.
}
