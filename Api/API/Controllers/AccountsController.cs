using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using API.JwtFeatures;
using Contracts;
using Contracts.Requests;
using Contracts.Responses;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationContext _appContext;
        private readonly JwtHandler _jwtHandler;

        public AccountsController(
            ApplicationContext appContext,
            JwtHandler jwtHandler)
        {
            _appContext = appContext;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = _appContext.Employees.SingleOrDefault(it => it.Email == loginRequest.Email);
            var isValidPassword = loginRequest?.Password == user.Password;
            if (user == null || !isValidPassword)
            {
                return Unauthorized(new LoginResponse
                {
                    ErrorMessage = "Invalid Authentication"
                });
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new LoginResponse
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            });
        }
    }
}
