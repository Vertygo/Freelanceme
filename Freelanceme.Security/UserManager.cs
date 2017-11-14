using Freelanceme.Domain;
using Freelanceme.Domain.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelanceme.Security
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserManager(UserManager<AppUser> userManager, IRepository<User> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<AppUser> FindByUsername(string username) =>
            await _userManager.FindByNameAsync(username);

        public async Task<IdentityResult> CreateUser(string name, string surname, string username, string email, string password)
        {
            var newUser = new User(name, surname, username, email);
            var appUser = new AppUser(newUser);
            var result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
            {
                return result;
            }

            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();
            await _userManager.AddClaimAsync(appUser, new Claim("UserId", newUser.Id.ToString()));
            await _userManager.UpdateAsync(appUser);

            return result;
        }

        public async Task<AppUser> GetUserAsync(ClaimsPrincipal principal) 
            => await _userManager.GetUserAsync(principal);
    }
}
