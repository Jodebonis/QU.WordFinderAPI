using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QU.WordFinderAPI.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace QU.WordFinderAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Provides functionality to generate a token.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpPost(Name = "Login")]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            // If Login Username and Password are correct then proceed to generate token.
            if (loginRequest.username != _config["User:Name"] || loginRequest.password != _config["User:Password"]) 
            {
                return BadRequest("Incorrect User or Password");
            }
            // Generate Token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}