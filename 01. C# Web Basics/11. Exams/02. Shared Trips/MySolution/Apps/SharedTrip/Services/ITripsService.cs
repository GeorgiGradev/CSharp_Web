using SharedTrip.ViewModels.Trips;
using System.Collections;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Create(AddTripInputModel trip);

        IEnumerable<TripViewModel> GetAll();

        TripDetailsViewModel GetDetails(string id);

        public bool AddUserToTrip(string userId, string tripId);
    }
}
