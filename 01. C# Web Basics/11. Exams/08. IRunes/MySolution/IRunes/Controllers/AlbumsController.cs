using IRunes.Services.Albums;
using IRunes.ViewModels.Albums;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.albumsService.GetAllAlbums();

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumCreateInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Name)
               || inputModel.Name.Length < 4
                  || inputModel.Name.Length > 20)
            {
                return this.Redirect("/Albums/Create");
            }

            if (string.IsNullOrEmpty(inputModel.Cover))
            {
                return this.Redirect("/Albums/Create");
            }

            this.albumsService.CreateAlbum(inputModel);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.albumsService.GetAlbumDetails(id);

            return this.View(viewModel);
        }


    }
}
