using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Freelancme.WebApi.V1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepo;
        private readonly IMapper _mapper;

        public ClientService(IMapper mapper,
            IRepository<Client> clientRepo)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task<Client> GetClientAsync(Guid clientId) =>
            await _clientRepo.GetAsync(clientId);

        public async Task<List<Client>> GetClientsAsync() =>
            await _clientRepo.GetAllAsync();
    }
}
