using Freelancme.WebApi.V1.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Freelancme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client")]
    [Route("api/client")]
    [Authorize]
    public class ClientController : Controller
    {
        [HttpGet("summary")]
        public IEnumerable<TimeTrackingSummary> GetTimeTrackingSummary()
        {
            yield return new TimeTrackingSummary { Client = "Ivan", WorkingTime = "1h", StartDate = DateTime.Today, EndDate = DateTime.Today };
            yield return new TimeTrackingSummary { Client = "Ivan A", WorkingTime = "2h", StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today };
            yield return new TimeTrackingSummary { Client = "Ivan B", WorkingTime = "3h", StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today };
            yield return new TimeTrackingSummary { Client = "Ivan C", WorkingTime = "4h", StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today };
        }
    }
}