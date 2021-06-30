namespace SharedTrip.ViewModels.Trips
{
    using System.Collections.Generic;

    public class AllTripsViewModel
    {
        public ICollection<TripViewModel> Trips { get; set; }
    }
}