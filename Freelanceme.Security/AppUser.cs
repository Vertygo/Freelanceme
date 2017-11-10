using Freelanceme.Domain;
using Microsoft.AspNetCore.Identity;

namespace Freelanceme.Security
{
    public class AppUser : IdentityUser
    {
        public AppUser(User user) : base(user.Username)
        {
            Email = user.Email;
        }

        private AppUser()
        {

        }
    }
}
