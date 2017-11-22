using Freelanceme.WebApi.V1.Dto.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Services.Interfaces
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> Login(LoginRequest request);

        Task<bool> CreateUser(RegisterRequest request);
    }
}