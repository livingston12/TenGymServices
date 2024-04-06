
using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public class CreatePlanCommand : CreatePlansDto, IRequest
    {
    
    }
}