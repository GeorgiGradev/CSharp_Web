using System.Collections.Generic;

namespace CarShop.ViewModels.Cars
{
    public class AllCarsViewModel
    {
        public ICollection<CarViewModel> Cars { get; set; }
    }
}
