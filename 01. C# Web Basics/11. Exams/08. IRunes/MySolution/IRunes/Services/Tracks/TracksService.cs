using IRunes.Data;
using IRunes.Data.Models;
using IRunes.ViewModels.Tracks;
using System;
using System.Linq;

namespace IRunes.Services.Tracks
{
    public class TracksService : ITracksService
    {
        private readonly ApplicationDbContext db;

        public TracksService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateTrack(string albumId, CreateTrackInputModel inputModel)
        {
            var track = new Track
            {
                Name = inputModel.Name,
                Link = inputModel.Link,
                Price = inputModel.Price,
                AlbumId = albumId
            };

            this.db.Tracks.Add(track);
            this.db.SaveChanges();
        }

        public TrackDetailsViewModel GetTrackDetails(string albumId, string trackId)
        {
            var viewModel = this.db.Tracks
                .Where(x => x.Id == trackId)
                .Select(x => new TrackDetailsViewModel
                {
                    Name = x.Name,
                    Price = x.Price.ToString("f2"),
                    Link = x.Link,
                    AlbumId = albumId
                }).FirstOrDefault();

            return viewModel;
        }

        public CreateTrackViewModel GetTrackViewModel(string albumId)
        {
            var viewModel = new CreateTrackViewModel
            {
                AlbumId = albumId
            };

            return viewModel;
        }
    }
}
