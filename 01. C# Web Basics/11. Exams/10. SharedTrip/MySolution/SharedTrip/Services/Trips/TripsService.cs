namespace SharedTrip.Services.Trips
{
    using System;
    using System.Linq;
    using System.Globalization;

    using SharedTrip.Data.Models;
    using SharedTrip.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTrip(AddTripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = input.Description,
                ImagePath = input.ImagePath,
                Seats = input.Seats
            };
            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool AddUserToTrip(string tripId, string userId)
        {
            var currentTrip = this.db.Trips
                .Where(x => x.Id == tripId)
                .FirstOrDefault();

            var availableSeats = currentTrip.Seats;

            bool isUserAtCurrentTrip = this.db.UserTrips
                .Any(x => x.TripId == tripId && x.UserId == userId);

            if (availableSeats == 0 || isUserAtCurrentTrip)
            {
                return false;
            }

            this.db.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = userId
            });
            this.db.SaveChanges();
            return true;
        }

        public AllTripsViewModel GetAllTrips()
        {
            var trips = this.db.Trips
                .Select(x => new TripViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    AvailableSeats = x.Seats - x.UserTrips.Count(),
                }).ToList();

            var viewModel = new AllTripsViewModel
            {
                Trips = trips
            };
            return viewModel;
        }

        public TripDetailsVewModel GetTripDetails(string id)
        {
            var tripViewModel = this.db.Trips
                .Where(x => x.Id == id)
                .Select(x => new TripDetailsVewModel
                {
                    Id = id,
                    DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    StartPoint = x.StartPoint,
                    AvailableSeats = x.Seats - x.UserTrips.Count(),
                    Description = x.Description,
                    EndPoint = x.EndPoint,
                    ImagePath = x.ImagePath
                }).FirstOrDefault();
            return tripViewModel;
        }

        public bool HasAvailableSeats(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                .Select(x => new { x.Seats, TakenSeats = x.UserTrips.Count()})
                .FirstOrDefault();
            var availableSeats = trip.Seats - trip.TakenSeats;
            return availableSeats > 0;
        }
    }
}