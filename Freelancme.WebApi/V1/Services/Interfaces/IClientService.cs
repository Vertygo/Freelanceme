using Freelanceme.WebApi.V1.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientInfo>> GetClientsAsync(bool projects, ClaimsPrincipal user);

        Task<Dto.Client> GetClientAsync(Guid clientId, ClaimsPrincipal user);

        Task<bool> SaveClientAsync(Dto.Client client, ClaimsPrincipal user);
    }
}
