using System;

namespace Rental.Infrastructure.Interfaces
{
    public interface IIdentityService
    {
        Guid GetUserIdentity();
        string GetUserEmail();
    }
}
