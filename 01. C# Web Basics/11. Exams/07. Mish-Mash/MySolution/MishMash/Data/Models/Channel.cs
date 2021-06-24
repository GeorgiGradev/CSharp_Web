using System;
using System.Collections.Generic;

namespace MishMash.Data
{
    public class Channel
    {
        public Channel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tags = new HashSet<ChannelTag>();
            this.Followers = new HashSet<UserChannel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ChannelType Type { get; set; }

        public virtual ICollection<ChannelTag> Tags { get; set; }

        public virtual ICollection<UserChannel> Followers { get; set; }
    }
}
