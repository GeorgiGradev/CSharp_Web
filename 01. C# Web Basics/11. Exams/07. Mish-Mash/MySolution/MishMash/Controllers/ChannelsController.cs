using MishMash.Services;
using MishMash.Services.Channels;
using MishMash.ViewModels.Channels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MishMash.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly IChannelsService channelsService;
        private readonly IUsersService usersService;

        public ChannelsController(
            IChannelsService channelsService,
            IUsersService usersService)
        {
            this.channelsService = channelsService;
            this.usersService = usersService;
        }


        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            if (!this.usersService.isUserAdmin(userId))
            {
                return this.Error("Only admins can create channel");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateChannelInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Name))
            {
                return this.Error("Invalid channel name");
            }

            if (string.IsNullOrEmpty(inputModel.Description))
            {
                return this.Error("Invalid channel description");
            }

            if (!this.channelsService.IsChannelNameAvailable(inputModel))
            {
                return this.Error("Channel name already in use");
            }

            this.channelsService.CreateChannel(inputModel);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.channelsService.GetChannelDetails(id);

            return this.View(viewModel);
        }

        public HttpResponse Follow(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            this.channelsService.FollowChannel(id, userId);

            return this.Redirect("/");
        }

        public HttpResponse Followed()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            var viewModel = this.channelsService.GetAllFollowedChannels(userId);

            return this.View(viewModel);
        }

        public HttpResponse Unfollow(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            this.channelsService.UnfollowChannel(id, userId);

            return this.Redirect("/Channels/Followed");
        }
    }
}
