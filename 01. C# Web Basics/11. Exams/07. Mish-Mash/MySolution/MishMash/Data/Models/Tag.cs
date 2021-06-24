using System;
using System.Collections.Generic;

namespace MishMash.Data
{
    public class Tag
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChannelTag> Channels { get; set; }
    }
}
