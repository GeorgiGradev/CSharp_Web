using IRunes.ViewModels.Albums;

namespace IRunes.Services.Albums
{
    public interface IAlbumsService
    {
        void CreateAlbum(AlbumCreateInputModel inputModel);

        AllAlbumsViewModel GetAllAlbums();

        AlbumDetailsViewModel GetAlbumDetails(string albumId);
    }
}
