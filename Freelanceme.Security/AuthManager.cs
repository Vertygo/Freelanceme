using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Freelanceme.Security
{
    public class AuthManager : IAuthManager
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AuthManager(SignInManager<AppUser> signInManager) =>
            _signInManager = signInManager;

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password) => 
            await _signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task<SignInResult> CheckPasswordSignIn(AppUser user, string password) =>
            await _signInManager.CheckPasswordSignInAsync(user, password, false);

        public async Task SignOut() => 
            await _signInManager.SignOutAsync();
    }
}
