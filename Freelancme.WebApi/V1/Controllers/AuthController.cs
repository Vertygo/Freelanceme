using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Freelanceme.Security;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;
using System.Collections.Generic;
using Freelancme.WebApi.V1.Dto;

namespace Freelancme.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config,
            IUserManager userManager,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
            _config = config;
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
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            var user = await _userManager.FindByUsername(login.Username);

            if (user != null)
            {
                var checkPwd = _authManager.CheckPasswordSignIn(user, login.Password);

                if (checkPwd.IsCompletedSuccessfully)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(30), // what about system integration?
                        signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }

            }

            return BadRequest("Invalid login attempt");
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
        public async Task<IActionResult> Register([FromBody]Register register)
        {
            var signinResult = await _userManager.CreateUser(register.Name, register.Surname, register.Username, register.Email, register.Password);

            if (signinResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest("Invalid registration attempt");
        }


        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
