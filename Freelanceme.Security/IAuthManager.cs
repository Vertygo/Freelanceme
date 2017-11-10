using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Freelanceme.Security
{
    public interface IAuthManager
    {
        Task<SignInResult> PasswordSignInAsync(string userName, string password);

        Task<SignInResult> CheckPasswordSignIn(AppUser user, string password);

        Task SignOut();
    }
}
