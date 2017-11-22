using Freelanceme.Security;
using Freelanceme.WebApi.V1.Dto.Request;
using Freelanceme.WebApi.V1.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Services
{
    public class AuthService : IAuthService
    {
        private IAuthManager _authManager;
        private IConfiguration _config;
        private IUserManager _userManager;

        public AuthService(IConfiguration config,
            IUserManager userManager,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
            _config = config;
        }

        public async Task<bool> CreateUser(RegisterRequest request)
        {
            var signinResult = await _userManager.CreateUser(request.Name, request.Surname, request.Username, request.Email, request.Password);

            return signinResult.Succeeded;
        }

        public async Task<JwtSecurityToken> Login(LoginRequest request)
        {
            var user = await _userManager.FindByUsername(request.Username);

            if (user != null)
            {
                var checkPwd = await _authManager.CheckPasswordSignIn(user, request.Password);

                if (checkPwd.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, user.UserName),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    return new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(30), // what about system integration?
                        signingCredentials: creds);

                }

            }

            return null;
        }
    }
}
