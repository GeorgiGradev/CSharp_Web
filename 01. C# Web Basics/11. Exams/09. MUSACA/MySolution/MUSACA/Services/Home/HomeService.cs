using MUSACA.Data;
using MUSACA.Data.Models;
using MUSACA.ViewModels.Home;
using System.Linq;

namespace MUSACA.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;

        public HomeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public LoginViewModel GetLoginViewModel(string userId)
        {
            var viewModel = new LoginViewModel
            {
                TotalPrice = this.db.Orders
                    .Where(x=>x.UserId == userId && x.Status == OrderStatus.Active)
                    .Select(x=>x.Products.Sum(y=>y.Product.Price)).FirstOrDefault().ToString("f2"),
                Products = this.db.Products
                    .Where(x => x.Orders.Any(y => y.Order.UserId == userId && y.Order.Status == OrderStatus.Active))
                    .Select(x => new ProductViewModel
                    {
                        Name = x.Name,
                        Price = x.Price.ToString("f2")
                    }).ToList()
            };

            return viewModel;
        }
    }
}
