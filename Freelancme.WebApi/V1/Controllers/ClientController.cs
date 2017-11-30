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
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <param name="projects">Output projects in result</param>
        /// <returns></returns>
        [HttpGet("list")]
        [Produces("application/json")]
        public async Task<IEnumerable<ClientInfo>> GetClientsAsync([FromQuery]bool projects)
            => await _clientService.GetClientsAsync(projects, User);

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [Produces("application/json")]
        public async Task<Client> GetClientAsync([FromQuery]Guid id)
            => await _clientService.GetClientAsync(id, User);

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost("save")]
        [Produces("application/json")]
        public async Task<bool> SaveClientAsync([FromBody]Client client)
            => await _clientService.SaveClientAsync(client, User);
    }
}