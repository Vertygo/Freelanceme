using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Freelanceme.Security;
using Freelanceme.WebApi.V1.Dto;
using Freelanceme.WebApi.V1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IUserManager _userManager;
        private readonly IRepository<Freelanceme.Domain.Client> _clientRepo;
        private readonly IMapper _mapper;

        public ClientService(IMapper mapper,
            IUserManager userManager,
            IRepository<User> userRepo,
            IRepository<Freelanceme.Domain.Client> clientRepo)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _userManager = userManager;
            _userRepo = userRepo;
        }

        public async Task<Dto.Client> GetClientAsync(Guid clientId, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);

            return _mapper.Map<Dto.Client>(await _clientRepo.GetAsync(f => f.Id == clientId && f.UserId == currentUser.Id, a => a.Address));
        }

        public async Task<List<ClientInfo>> GetClientsAsync(bool projects, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);

            if (projects)
                return _mapper.Map<List<ClientInfo>>(await _clientRepo.GetFilteredAsync(f => f.UserId == currentUser.Id, a => a.Address, p => p.Projects)); 
            else
                return _mapper.Map<List<ClientInfo>>(await _clientRepo.GetFilteredAsync(f => f.UserId == currentUser.Id, a => a.Address));
        }

        public async Task<bool> SaveClientAsync(Dto.Client client, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            var obj = _mapper.Map<Freelanceme.Domain.Client>(client);

            if (client.Id == Guid.Empty)
            {
                obj.UserId = currentUser.Id;
                _clientRepo.Add(obj);
                obj.Projects = new List<Project>
                {
                    new Project { Name = "Project 1", IsActive = true },
                    new Project { Name = "Project 2", IsActive = true }
                };
            }
            else
            {
                _clientRepo.Update(obj);
            }

            return await _clientRepo.SaveChangesAsync();
        }
    }
}
