using MishMash.Data;
using MishMash.ViewModels.Channels;
using MishMash.ViewModels.Home;
using System.Collections.Generic;
using System.Linq;

namespace MishMash.Services.Home
{
    class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;

        public HomeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public HomeViewModel GetLoginDetails(string userId)
        {
            var yourChannels = this.db.Channels
                .Where(x => x.Followers.Any(y => y.UserId == userId))
                .Select(x => new BaseChannelViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    FollowersCount = x.Followers.Count,
                }).ToList();

            var followedChannels = this.db.Channels
                .Where(x => x.Followers.Any(x => x.UserId == userId)).ToList();

            var followedTags = new List<string>();

            foreach (var channel in followedChannels)
            {
                foreach (var tag in channel.Tags.Select(x => x.Tag.Name))
                {
                    if (!followedTags.Contains(tag))
                    {
                        followedTags.Add(tag);
                    }
                }
            }

            var suggestedChannelsAsList = new List<Channel>();

            foreach (var tag in followedTags)
            {
                var tagId = this.db.Tags.Where(x => x.Name == tag).Select(x=>x.Id).FirstOrDefault();

                foreach (var channel in this.db.Channels)
                {
                    if (channel.Tags.Any(x=>x.TagId == tagId))
                    {
                        suggestedChannelsAsList.Add(channel);
                    }
                }
            }

            var suggestedChannels = suggestedChannelsAsList
                .Where(x=>!x.Followers.Any(y=>y.UserId == userId))
                .Select(x => new BaseChannelViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    FollowersCount = x.Followers.Count
                }).ToList();


            var seeOtherChannelsAsList = new List<Channel>();

            var myChannelsAsList = this.db.Channels
                .Where(x => x.Followers.Any(y => y.UserId == userId)).ToList();

            foreach (var channel in this.db.Channels)
            {
                if (!(myChannelsAsList.Contains(channel) 
                    || seeOtherChannelsAsList.Contains(channel)))
                {
                    seeOtherChannelsAsList.Add(channel);
                }
            }

            var seeOther = seeOtherChannelsAsList
                .Select(x => new BaseChannelViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    FollowersCount = x.Followers.Count
                }).ToList();

            var viewModel = this.db.Users
                .Where(x => x.Id == userId)
                .Select(x => new HomeViewModel
                {
                    Username = x.Username,
                    UserRole = x.Role.ToString(),
                    YourChannels = yourChannels,
                    SuggestedChannels = suggestedChannels,
                    SeeOther = seeOther,
                }).FirstOrDefault();

            ;
            return viewModel;
        }
    }
}
