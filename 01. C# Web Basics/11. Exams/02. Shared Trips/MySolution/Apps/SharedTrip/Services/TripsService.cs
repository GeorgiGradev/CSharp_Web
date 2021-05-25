using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public bool AddUserToTrip(string userId, string tripId)
        {
            if (this.db.UserTrips
                    .Any(ut => ut.UserID == userId &&
                                          ut.TripId == tripId) ||
                this.db.Trips
                    .Where(t => t.Id == tripId)
                    .Select(t => t.Seats - t.UserTrips.Count)
                    .FirstOrDefault() == 0)
            {
                return false;
            }

            this.db.UserTrips.Add(new UserTrip()
            {
                TripId = tripId,
                UserID = userId
            });

            this.db.SaveChanges();

            return true;
        }

        public void Create(AddTripInputModel trip)
        {
            var dbTrip = new Trip
            {
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = DateTime.ParseExact(trip.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = trip.ImagePath,
                Seats = trip.Seats,
                Description = trip.Description
            };

            this.db.Trips.Add(dbTrip);
            db.SaveChanges();
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = this.db.Trips.Select(x => new TripViewModel
            {
                DepartureTime = x.DepartureTime,
                EndPoint = x.EndPoint,
                StartPoint = x.StartPoint, 
                Id = x.Id,
                AvailableSeats = x.Seats - x.UserTrips.Count()
            }).ToList();

            return trips;
        }

        public TripDetailsViewModel GetDetails(string id)
        {
            var trip = this.db.Trips.Where(x => x.Id == id)
                .Select(x=> new TripDetailsViewModel()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,
                    Seats = x.Seats,
                    Description = x.Description,
                }).FirstOrDefault();
            return trip;
        }
    }
}
