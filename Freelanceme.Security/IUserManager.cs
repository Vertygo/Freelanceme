﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freelanceme.Security
{
    public interface IUserManager
    {
        Task<AppUser> GetUserAsync(ClaimsPrincipal principal);

        Task<AppUser> FindByUsername(string username);

        Task<IdentityResult> CreateUser(string name, string surname, string username, string email, string password);
    }
}
