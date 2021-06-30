namespace SharedTrip.Services.Trips
{
    using SharedTrip.ViewModels.Trips;

    public interface ITripsService
    {
        void AddTrip(AddTripInputModel input);

        AllTripsViewModel GetAllTrips();

        TripDetailsVewModel GetTripDetails(string id);

        bool AddUserToTrip(string tripId, string userId);

        bool HasAvailableSeats(string tripId);
    }
}
