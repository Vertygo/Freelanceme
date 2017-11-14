using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Freelanceme.Security;
using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Dto.Request;
using Freelancme.WebApi.V1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelancme.WebApi.V1.Services
{
    public class ClientService : IClientService
    {
        IUserManager _userManager;
        IRepository<TimeTracking> _timeTrackingRepo;
        IMapper _mapper;
        IRepository<User> _userRepo;

        public ClientService(IUserManager userManager,
            IMapper mapper,
            IRepository<TimeTracking> timeTrackingRepo,
            IRepository<User> userRepo)
        {
            _userManager = userManager;
            _timeTrackingRepo = timeTrackingRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Display general time tracking information for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary(TimeTrackingSummaryRequest request, ClaimsPrincipal user)
        {
            var username = (await _userManager.GetUserAsync(user)).UserName;
            var currentUser  = _userRepo.GetFiltered(u => u.Username == username).FirstOrDefault(); // we should have same guid in both tables?
            var timeTrackingList = _timeTrackingRepo.GetFiltered(f => f.User.Id == currentUser.Id && f.Date >= request.DateFrom.Date && f.Date <= request.DateTo.Date, e => e.Client, e => e.User).ToList();

            return timeTrackingList.GroupBy(x => x.Client)
                .Select(g => new TimeTrackingSummary
                {
                    ClientID = g.Key.Id,
                    Client = $"{g.Key.Name} {g.Key.Surname}",
                    EndDate = g.Max(m => m.Date),
                    StartDate = g.Min(m => m.Date),
                    WorkingHours = g.Sum(s => s.WorkingHours)
                }).ToList();
        }

        /// <summary>
        /// Display detailed information about time tracking for each client
        /// </summary>
        /// <returns></returns>
        [HttpGet("details")]
        [Produces("application/json")]
        public async Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails(TimeTrackingDetailRequest request, ClaimsPrincipal user)
        {
            var username = (await _userManager.GetUserAsync(user)).UserName;
            var currentUser = _userRepo.GetFiltered(u => u.Username == username).FirstOrDefault(); // we should have same guid in both tables?
            var timeTrackingList = _timeTrackingRepo.GetFiltered(f => f.User.Id == currentUser.Id && f.Date >= request.StartDate.Date && f.Date <= request.EndDate.Date && f.Client.Id == request.ClientID,
                e => e.Client,
                e => e.User);

            return _mapper.Map<IEnumerable<TimeTrackingDetails>>(timeTrackingList);
        }
    }
}
