using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            //JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName), //given_name
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Language", "English"),
            };
            var claimsIdentity = new ClaimsIdentity(claims);

            var privateKey = _configuration["privateKey"];
            var expirationTime = _configuration.GetValue<int>("expirationHours");
            var issuer = _configuration["issuer"];
            var audience = _configuration["audience"];

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwtExpirationTime = DateTime.UtcNow.AddHours(expirationTime);

            var tokenDescription = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = claimsIdentity,
                Issuer = issuer,
                Audience = audience,
                Expires = jwtExpirationTime,
            };

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.CreateToken(tokenDescription);
            return Ok(new { token = handler.WriteToken(jwtToken) });
        
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }
    }
}