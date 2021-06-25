using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels;
using CarShop.ViewModels.Cars;
using System.Linq;

namespace CarShop.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCar(string userId, CarInputModel inputModel)
        {
            var car = new Car
            {
                Model = inputModel.Model,
                PictureUrl = inputModel.Image,
                PlateNumber = inputModel.PlateNumber,
                Year = inputModel.Year,
                OwnerId = userId
            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public AllCarsViewModel GetAllCars(string userId)
        {
            var viewModel = new AllCarsViewModel
            {
                Cars = this.db.Cars
                    .Where(x => x.OwnerId == userId)
                    .Select(x => new CarViewModel
                    {
                        PlateNumber = x.PlateNumber,
                        Image = x.PictureUrl,
                        Model = x.Model,
                        FixedIssues = x.Issues.Where(y => y.IsFixed).Count(),
                        RemainingIssues = x.Issues.Where(y => !y.IsFixed).Count(),
                        Id = x.Id
                    }).ToList()
            };

            return viewModel;
        }

        public AllCarsViewModel GetAllCarsForNechanics()
        {
            var viewModel = new AllCarsViewModel
            {
                Cars = this.db.Cars
                    .Where(x => x.Issues.Count > 0)
                    .Select(x => new CarViewModel
                    {
                        PlateNumber = x.PlateNumber,
                        Image = x.PictureUrl,
                        Model = x.Model,
                        FixedIssues = x.Issues.Where(y => y.IsFixed).Count(),
                        RemainingIssues = x.Issues.Where(y => !y.IsFixed).Count(),
                        Id = x.Id
                    }).ToList()
            };

            return viewModel;
        }
    }
}
