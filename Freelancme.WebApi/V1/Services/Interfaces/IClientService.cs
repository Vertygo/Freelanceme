using Freelanceme.Domain;
using Freelancme.WebApi.V1.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientInfo>> GetClientsAsync(bool projects);

        Task<Dto.Client> GetClientAsync(Guid clientId);

        Task<bool> SaveClientAsync(Dto.Client client);
    }
}
