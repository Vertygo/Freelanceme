using Freelanceme.Domain;
using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Dto.Request;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary(TimeTrackingSummaryRequest request, ClaimsPrincipal user);

        Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails(TimeTrackingDetailRequest request, ClaimsPrincipal user);
    }
}
