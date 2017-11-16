using AutoMapper;
using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Freelanceme.Security;
using Freelancme.WebApi.V1.Dto;
using Freelancme.WebApi.V1.Dto.Request;
using Freelancme.WebApi.V1.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Freelancme.WebApi.V1.Services
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<TimeTracking> _timeTrackingRepo;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepo;

        public TimeTrackingService(IUserManager userManager,
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
        /// <param name="request"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TimeTrackingSummary>> GetTimeTrackingSummary(TimeTrackingSummaryRequest request, ClaimsPrincipal user)
        {
            var username = (await _userManager.GetUserAsync(user)).UserName;
            var currentUser = (await _userRepo.GetFilteredAsync(u => u.Username == username)).FirstOrDefault(); // we should have same guid in both tables?
            var timeTrackingList = await _timeTrackingRepo.GetFilteredAsync(f => f.User.Id == currentUser.Id && f.Date >= request.DateFrom.Date && f.Date <= request.DateTo.Date, e => e.Client, e => e.User);

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
        /// <param name="request"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TimeTrackingDetails>> GetTimeTrackingDetails(TimeTrackingDetailRequest request, ClaimsPrincipal user)
        {
            var username = (await _userManager.GetUserAsync(user)).UserName;
            var currentUser = (await _userRepo.GetFilteredAsync(u => u.Username == username)).FirstOrDefault(); // we should have same guid in both tables?
            var timeTrackingList = await (_timeTrackingRepo.GetFilteredAsync(f => f.User.Id == currentUser.Id && f.Date >= request.StartDate.Date && f.Date <= request.EndDate.Date && f.Client.Id == request.ClientID,
                e => e.Client,
                e => e.User));

            return _mapper.Map<IEnumerable<TimeTrackingDetails>>(timeTrackingList);
        }
    }
}