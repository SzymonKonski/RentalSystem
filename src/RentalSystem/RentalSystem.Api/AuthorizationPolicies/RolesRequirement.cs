using Microsoft.AspNetCore.Authorization;

namespace RentalSystem.Api.AuthorizationPolicies
{
    public class RolesRequirement : IAuthorizationRequirement
    {
        public readonly string RoleName;

        public RolesRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
