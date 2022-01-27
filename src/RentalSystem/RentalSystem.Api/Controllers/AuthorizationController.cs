using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rental.Infrastructure;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Api.JwtFeatures;


namespace RentalSystem.Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize(Policy = "AccessAsUser")]
    [ApiController]
    public class Authorization : Controller
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly RentalCarDbContext rentalCarDbContext;
        private readonly JwtHandler jwtHandler;

        public Authorization(RentalCarDbContext rentalCarDbContext, IHttpContextAccessor httpContext, JwtHandler jwtHandler)
        {
            this.rentalCarDbContext = rentalCarDbContext;
            this.httpContext = httpContext;
            this.jwtHandler = jwtHandler;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var group = await rentalCarDbContext.UserAuthorizationGroups.FirstOrDefaultAsync(x => x.UserId.ToString() == userId);
            var claims = new List<Claim>();

            if (group == null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Viewer"));
            }
            else
            {
                var groupName = await rentalCarDbContext.AuthorizationGroups.FirstOrDefaultAsync(x => x.Id == group.GroupId);
                claims.Add(new Claim(ClaimTypes.Role, groupName.GroupName));
            }

            var signingCredentials = jwtHandler.GetSigningCredentials();
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }

    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
