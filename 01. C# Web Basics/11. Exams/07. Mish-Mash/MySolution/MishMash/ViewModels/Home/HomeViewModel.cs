using MishMash.ViewModels.Channels;
using System.Collections.Generic;

namespace MishMash.ViewModels.Home
{
    public class HomeViewModel
    {
        public string UserRole { get; set; }

        public string Username { get; set; }

        public ICollection<BaseChannelViewModel> YourChannels { get; set; }

        public ICollection<BaseChannelViewModel> SuggestedChannels { get; set; }

        public ICollection<BaseChannelViewModel> SeeOther { get; set; }
    }
}
