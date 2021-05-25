using System;

namespace SharedTrip.ViewModels.Trips
{
    public class TripDetailsViewModel
    {
        public string Id { get; set; }

        public string ImagePath { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DepartureTimeFormated => DepartureTime.ToString("s");

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
