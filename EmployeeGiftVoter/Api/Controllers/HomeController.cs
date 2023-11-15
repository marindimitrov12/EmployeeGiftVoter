using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IConfiguration _configuration;
        public HomeController(IClientService clientService, IConfiguration configuration)
        {
            this._clientService = clientService;
            this._configuration = configuration;
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(ClientResponseDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LogInClientDto user)
        {
            var result=new ClientResponseDto();
            try
            {
                 result= await _clientService.Login(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            result.AccessToken = CreateToken(result);
            return Ok(result); 
        }
        [HttpPost("register")]
        [ProducesResponseType(typeof(ClientResponseDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterClientDto user)
        {
            var result = new ClientResponseDto();
            try
            {
                result = await _clientService.Register(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            result.AccessToken = CreateToken(result);
            return Ok(result);
        }
        private string CreateToken(ClientResponseDto company)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Employee")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
