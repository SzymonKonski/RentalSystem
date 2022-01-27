using System;

namespace RentalSystem.Domain.Entities
{
    public class UserAuthorizationGroups
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int GroupId { get; set; }
    }
}
