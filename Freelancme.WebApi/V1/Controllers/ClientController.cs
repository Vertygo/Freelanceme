using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Dto.Request;
using Freelancme.WebApi.V1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client")]
    [Route("api/client")]
    [Authorize]
    public class ClientController : Controller
    {

        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary([FromQuery]TimeTrackingSummaryRequest request)
            => await _clientService.GetTimeTrackingSummary(request, User);

        /// <summary>
        /// Display detailed information about time tracking for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("details")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails([FromQuery]TimeTrackingDetailRequest request)
            => await _clientService.GetTimeTrackingDetails(request, User);
    }
}