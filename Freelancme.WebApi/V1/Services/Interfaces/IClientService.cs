using Freelanceme.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetClientsAsync();

        Task<Client> GetClientAsync(Guid clientId);
    }
}
