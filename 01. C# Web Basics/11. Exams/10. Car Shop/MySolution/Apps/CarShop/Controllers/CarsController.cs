using CarShop.Services;
using CarShop.Services.Cars;
using CarShop.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Text.RegularExpressions;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public CarsController(
            ICarsService carService,
            IUsersService usersService)
        {
            this.carsService = carService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            string userId = this.GetUserId();
            var isUserMechanic = this.usersService.IsUserMechanic(userId);

            if (isUserMechanic)
            {
                var viewModel = this.carsService.GetAllCarsForNechanics();
                return this.View(viewModel);
            }
            else
            {
                var viewModel = this.carsService.GetAllCars(userId);
                return this.View(viewModel);
            }
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CarInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Model)
                || inputModel.Model.Length < 5
                || inputModel.Model.Length > 20)
            {
                return this.Error("Model should be between 5 and 20 characters");
            }

            if (inputModel.Year < 1900)
            {
                return this.Error("Year should have a value");
            }

            if (string.IsNullOrEmpty(inputModel.Image))
            {
                return this.Error("Please add an image!");
            }

            if (!Regex.IsMatch(inputModel.PlateNumber, @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$"))
            {
                return this.Error("Please add a valid plate number!");
            }

            var userId = this.GetUserId();

            if (usersService.IsUserMechanic(userId) == true)
            {
                return this.Error("Mechanics can not add cars");
            }
            
            this.carsService.AddCar(userId, inputModel);

            return this.Redirect("/Cars/All");
        }
    }
}
