using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Freelancme.WebApi.V1.Services.Interfaces;
using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Dto.Request;

namespace Freelancme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/timetracking")]
    [Route("api/timetracking")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class TimeTrackingController : Controller
    {

        private ITimeTrackingService _timeTrackingService;

        public TimeTrackingController(ITimeTrackingService timeTrackingService)
        {
            _timeTrackingService = timeTrackingService;
        }

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary([FromQuery]TimeTrackingSummaryRequest request)
            => await _timeTrackingService.GetTimeTrackingSummary(request, User);

        /// <summary>
        /// Display detailed information about time tracking for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("details")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails([FromQuery]TimeTrackingDetailRequest request)
            => await _timeTrackingService.GetTimeTrackingDetails(request, User);

        /// <summary>
        /// Display detailed information about time tracking for each client
        /// </summary>
        /// <returns></returns>
        [HttpPost("save")]
        [Produces("application/json")]
        public async Task<bool> SaveTimeLog([FromBody]TimeLog request)
            => await _timeTrackingService.SaveTimeLogAsync(request, User);
    }
}