using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Rental.Infrastructure;

namespace RentalSystem.Api.AuthorizationPolicies
{
    public class RoleHandler : AuthorizationHandler<RolesRequirement>
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly RentalCarDbContext rentalCarDbContext;

        public RoleHandler(RentalCarDbContext rentalCarDbContext, IHttpContextAccessor httpContext)
        {
            this.rentalCarDbContext = rentalCarDbContext;
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
        {
            var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userAuthorizationGroup = rentalCarDbContext.UserAuthorizationGroups.FirstOrDefault(x => x.UserId.ToString() == userId);

            if (userAuthorizationGroup == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var group = rentalCarDbContext.AuthorizationGroups.FirstOrDefault(x => x.Id == userAuthorizationGroup.GroupId);

            if (group == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (group.GroupName != requirement.RoleName)
                context.Fail();
            else
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
