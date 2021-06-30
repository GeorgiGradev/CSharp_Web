namespace SharedTrip.Controllers
{
    using System;
    using System.Globalization;

    using SharedTrip.Services.Trips;
    using SharedTrip.ViewModels.Trips;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.StartPoint)
                || string.IsNullOrWhiteSpace(input.StartPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrEmpty(input.EndPoint)
                || string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return this.Redirect("/Trips/Add");
            }
            if (!DateTime.TryParseExact(
                    input.DepartureTime,
                    "dd.MM.yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out _))
            {
                return this.Redirect("/Trips/Add");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.Redirect("/Trips/Add");
            }

            this.tripsService.AddTrip(input);
            return this.Redirect("/");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetAllTrips();
            return this.View(viewModel);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetTripDetails(tripId);
            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            var userId = this.GetUserId();
            var addUser = this.tripsService.AddUserToTrip(tripId, userId);

            if (addUser == true)
            {
                return this.Redirect("/Trips/All");
            }

            return this.Redirect($"/Trips/Details?tripId={tripId}");
        }
    }
}
