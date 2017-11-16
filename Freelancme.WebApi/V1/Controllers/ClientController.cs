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

    }
}