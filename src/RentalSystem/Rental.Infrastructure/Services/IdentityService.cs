using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rental.Infrastructure.Interfaces;

namespace Rental.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor context;

        public IdentityService(IHttpContextAccessor context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guid GetUserIdentity()
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return new Guid(userId);
        }

        public string GetUserEmail()
        {
            var userEmail = context.HttpContext.User.FindFirst("emails").Value;
            return userEmail;
        }
    }
}
