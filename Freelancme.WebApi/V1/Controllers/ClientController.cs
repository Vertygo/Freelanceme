using Freelanceme.WebApi.V1.Dto;
using Freelanceme.WebApi.V1.Dto.Request;
using Freelanceme.WebApi.V1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client")]
    [Route("api/client")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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
        [HttpGet("list")]
        [Produces("application/json")]
        public async Task<IEnumerable<ClientInfo>> GetClients([FromQuery]bool projects)
            => await _clientService.GetClientsAsync(projects, User);

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        [Produces("application/json")]
        public async Task<Client> GetClient([FromQuery]Guid id)
            => await _clientService.GetClientAsync(id, User);

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <returns></returns>
        [HttpPost("save")]
        [Produces("application/json")]
        public async Task<bool> SaveClient([FromBody]Client client)
            => await _clientService.SaveClientAsync(client, User);
    }
}