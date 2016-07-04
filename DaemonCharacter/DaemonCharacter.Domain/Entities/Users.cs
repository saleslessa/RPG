
using System;

namespace DaemonCharacter.Domain.Entities
{
    public class Users
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public UserProfileStatus UserProfile { get; set; }

        public Users()
        {
            UserId = new Guid();
        }
    }
}
