using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Freelanceme.Domain.Client> _clientRepo;
        private readonly IRepository<Freelanceme.Domain.Project> _projectRepo;
        private readonly IMapper _mapper;

        public ClientService(IMapper mapper,
            IRepository<Freelanceme.Domain.Client> clientRepo,
            IRepository<Freelanceme.Domain.Project> projectRepo)
        {
            _clientRepo = clientRepo;
            _projectRepo = projectRepo;
            _mapper = mapper;
        }

        public async Task<Dto.Client> GetClientAsync(Guid clientId) =>
            _mapper.Map<Dto.Client>(await _clientRepo.GetAsync(clientId, p => p.Address));


        public async Task<List<ClientInfo>> GetClientsAsync(bool projects)
        {
            if (projects)
                return _mapper.Map<List<ClientInfo>>(await _clientRepo.GetAllAsync(a => a.Address, p => p.Projects)); 
            else
                return _mapper.Map<List<ClientInfo>>(await _clientRepo.GetAllAsync(a => a.Address));
        }

        public async Task<bool> SaveClientAsync(Dto.Client client)
        {
            var obj = _mapper.Map<Freelanceme.Domain.Client>(client);

            if (client.Id == Guid.Empty)
            {
                _clientRepo.Add(obj);
                obj.Projects = new List<Project> { new Project { Name = "Project 1", IsActive = true } };
            }
            else
            {
                _clientRepo.Update(obj);
            }

            return await _clientRepo.SaveChangesAsync();
        }
    }
}
