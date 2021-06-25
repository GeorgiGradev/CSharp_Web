using System.Collections.Generic;

namespace MUSACA.ViewModels.Users
{
    public class AllUserProfileViewModel
    {
        public string Username { get; set; }

        public ICollection<UserProfileViewModel> Profiles { get; set; }
    }
}
