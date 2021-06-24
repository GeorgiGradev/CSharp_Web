using System;
using System.Collections.Generic;

namespace MishMash.Data
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.FollowedChannels = new HashSet<UserChannel>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<UserChannel> FollowedChannels { get; set; }
    }
}
