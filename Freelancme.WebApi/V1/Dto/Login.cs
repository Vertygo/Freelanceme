using System.ComponentModel.DataAnnotations;

namespace Freelancme.WebApi.V1.Dto
{
    public class Login
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
