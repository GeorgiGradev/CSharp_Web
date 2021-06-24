using MishMash.ViewModels.Channels;

namespace MishMash.Services.Channels
{
    public interface IChannelsService
    {
        void CreateChannel(CreateChannelInputModel inputModel);

        bool IsChannelNameAvailable(CreateChannelInputModel inputModel);

        ChannelDetailsViewModel GetChannelDetails(string channelId);

        void FollowChannel(string channelId, string userId);

        AllFollowedChannelsViewModel GetAllFollowedChannels(string userId);

        void UnfollowChannel(string channelId, string userId);
    }
}
