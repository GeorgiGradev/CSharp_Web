using IRunes.Services.Tracks;
using IRunes.ViewModels.Tracks;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }


        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.tracksService.GetTrackViewModel(albumId);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string albumId, CreateTrackInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Name)
               || inputModel.Name.Length < 4
               || inputModel.Name.Length > 20)
            {
                return this.Redirect($"/Tracks/Create?albumId={albumId}");
            }

            if (string.IsNullOrEmpty(inputModel.Link))
            {
                return this.Redirect($"/Tracks/Create?albumId={albumId}");
            }

            if (inputModel.Price < 0)
            {
                return this.Redirect($"/Tracks/Create?albumId={albumId}");
            }

            this.tracksService.CreateTrack(albumId, inputModel);

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.tracksService.GetTrackDetails(albumId, trackId);

            return this.View(viewModel);
        }

    }
}
