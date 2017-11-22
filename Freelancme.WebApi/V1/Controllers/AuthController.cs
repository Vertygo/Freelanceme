using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Freelanceme.WebApi.V1.Services.Interfaces;
using Freelanceme.WebApi.V1.Dto.Request;

namespace Freelanceme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="login">Login object</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/auth/login
        ///     {
        ///        "Username": "ivanm",
        ///        "Password": "abcd"
        ///     }
        ///
        /// </remarks>
        /// <returns>JWT Token if login is successful</returns>
        /// <response code="400">If the login is unsuccessful</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 403)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Login([FromBody]LoginRequest login)
        {
            var token = await _authService.Login(login);

            if (token != null)
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            else
                return NotFound("Invalid login attempt");
        }

        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="register">Registration object</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/auth/register
        ///     {
        ///        "Name": "Ivan",
        ///        "Surname": "Milosavljevic",
        ///        "Email": "verthygo@gmail.com",
        ///        "Username": "ivanm",
        ///        "Password": "abcd"
        ///     }
        ///
        /// </remarks>
        /// <returns>JWT Token if login is successful</returns>
        /// <response code="400">If the login is unsuccessful</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 403)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            var isCreated = await _authService.CreateUser(request);

            return isCreated ? (IActionResult)Ok() : BadRequest("Invalid registration attempt");
        }
    }
}
