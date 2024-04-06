namespace TenGymServices.Api.Plans.Core.Enums;

public enum SETUP_FEE_FAILURE_ACTION
{
    CONTINUE,    // Continues the subscription if the initial payment for the setup fails.
    CANCEL  // Cancels the subscription if the initial payment for the setup fails.
}
