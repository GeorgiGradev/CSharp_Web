using CarShop.ViewModels;
using CarShop.ViewModels.Cars;

namespace CarShop.Services.Cars
{
    public interface ICarsService
    {
        public void AddCar(string userId, CarInputModel inputModel);

        AllCarsViewModel GetAllCars(string userId);

        AllCarsViewModel GetAllCarsForNechanics();
    }
}
