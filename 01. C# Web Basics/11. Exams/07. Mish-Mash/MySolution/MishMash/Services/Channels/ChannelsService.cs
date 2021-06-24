using MishMash.Data;
using MishMash.ViewModels.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MishMash.Services.Channels
{
    public class ChannelsService : IChannelsService
    {
        private readonly ApplicationDbContext db;

        public ChannelsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateChannel(CreateChannelInputModel inputModel)
        {
            var channel = new Channel
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Type = Enum.Parse<ChannelType>(inputModel.Type),
            };
            this.db.Add(channel);
            this.db.SaveChanges();

            var tagsAsString = inputModel.Tags
                .Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var channelTags = new List<ChannelTag>();

            foreach (var tagName in tagsAsString)
            {
                var tag = new Tag();

                if (!this.db.Tags.Any(x=>x.Name == tagName))
                {
                    tag = new Tag
                    {
                        Name = tagName
                    };
                    this.db.Tags.Add(tag);
                    this.db.SaveChanges();
                } 
                else
                {
                    tag = this.db.Tags
                        .Where(x => x.Name == tagName)
                        .FirstOrDefault();
                }

                var channelTag = new ChannelTag
                {
                    Channel = channel,
                    Tag = tag,
                };

                this.db.ChannelTags.Add(channelTag);
                this.db.SaveChanges();

                channelTags.Add(channelTag);
            }

            channel.Tags = channelTags;
        }

        public void FollowChannel(string channelId, string userId)
        {
            var userChannel = new UserChannel
            {
                ChannelId = channelId,
                UserId = userId
            };

            this.db.UserChannels.Add(userChannel);
            this.db.SaveChanges();
        }

        public AllFollowedChannelsViewModel GetAllFollowedChannels(string userId)
        {
            var viewModel = new AllFollowedChannelsViewModel
            {
                FollowedChannels = this.db.Channels
                .Where(x => x.Followers.Any(y => y.UserId == userId))
                .Select(x => new BaseChannelViewModel
                {
                    Name = x.Name,
                    Type = x.Type.ToString(),
                    Id = x.Id,
                    FollowersCount = x.Followers.Count,
                }).ToList()
            };

            return viewModel;
        }

        public ChannelDetailsViewModel GetChannelDetails(string channelId)
        {
            var viewModel = this.db.Channels
                .Where(x => x.Id == channelId)
                .Select(x => new ChannelDetailsViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.Type.ToString(),
                    FollowersCount = x.Followers.Count,
                    TagsAsString = string.Join(", ", x.Tags.Select(x=>x.Tag.Name))
                }).FirstOrDefault();

            return viewModel;
        }

        public bool IsChannelNameAvailable(CreateChannelInputModel inputModel)
        {
            return !db.Channels.Any(x => x.Name == inputModel.Name);
        }

        public void UnfollowChannel(string channelId, string userId)
        {
            var userChannel = this.db.UserChannels
                .Where(x => x.UserId == userId && x.ChannelId == channelId)
                .FirstOrDefault();

            this.db.UserChannels.Remove(userChannel);
            this.db.SaveChanges();
        }
    }
}
