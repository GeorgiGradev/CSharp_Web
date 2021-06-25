using IRunes.Data.Models;
using System.Collections.Generic;

namespace IRunes.ViewModels.Albums
{
    public class AlbumDetailsViewModel
    {
        public string Cover { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Id { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
