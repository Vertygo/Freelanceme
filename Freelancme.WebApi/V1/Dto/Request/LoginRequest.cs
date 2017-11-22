using System.ComponentModel.DataAnnotations;

namespace Freelanceme.WebApi.V1.Dto.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
