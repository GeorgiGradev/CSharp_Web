using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }
            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }


            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Enter Start Point.");
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("Enter End Point.");
            }

            if (!DateTime.TryParseExact(
                input.DepartureTime, 
                "dd.MM.yyyy HH:mm", 
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
            {
                return this.Error("Enter valid DateTime - dd.MM.yyyy HH: mm");
            }


            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats should be bewteen 2 and 6.");
            }

            if (string.IsNullOrEmpty(input.Description)
                || input.Description.Length > 80)
            {
                return this.Error("Description should have maximum 80 charachters");
            }


            this.tripsService.Create(input); // подаваме готовият модел и той го създава
            // проверяваме в заданието накъде да насочим потребителя

            return this.Redirect("/Trips/All");
            // горе създаваме HttpReponse All(), което да връща VIEW-то
        }


        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }

            var trip = this.tripsService.GetDetails(tripId);
            return this.View(trip); 
        }


        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (!this.tripsService.AddUserToTrip(userId, tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }


            return this.Redirect("/Trips/All");
        }
    }
}
