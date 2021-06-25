using System.Collections.Generic;

namespace MUSACA.ViewModels.Home
{
    public class LoginViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }

        public string TotalPrice { get; set; }
    }
}
