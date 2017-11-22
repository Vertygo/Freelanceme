using Freelanceme.WebApi.V1.Dto;
using Freelanceme.WebApi.V1.Dto.Request;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary(TimeTrackingSummaryRequest request, ClaimsPrincipal user);

        Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails(TimeTrackingDetailRequest request, ClaimsPrincipal user);

        Task<bool> SaveTimeLogAsync(TimeLog timeLog, ClaimsPrincipal user);
    }
}
