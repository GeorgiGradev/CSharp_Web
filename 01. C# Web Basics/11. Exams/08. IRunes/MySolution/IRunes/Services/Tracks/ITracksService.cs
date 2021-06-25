using IRunes.ViewModels.Tracks;

namespace IRunes.Services.Tracks
{
    public interface ITracksService
    {
        CreateTrackViewModel GetTrackViewModel(string albumId);

        void CreateTrack(string albumId, CreateTrackInputModel inputModel);

        TrackDetailsViewModel GetTrackDetails(string albumId, string trackId);
    }
}
