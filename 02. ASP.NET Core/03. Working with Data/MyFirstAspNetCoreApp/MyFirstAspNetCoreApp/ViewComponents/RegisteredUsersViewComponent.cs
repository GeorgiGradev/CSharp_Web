using Microsoft.AspNetCore.Mvc;
using MyFirstAspNetCoreApp.Data;
using MyFirstAspNetCoreApp.ViewModels.ViewComponents;
using System.Linq;

namespace MyFirstAspNetCoreApp.ViewComponents
{
    public class RegisteredUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public RegisteredUsersViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke(string title)
        {
            var viewModel = new RegisteredUsersViewModel
            {
                Title = title,
                Users = this.db.Users.Count()
            };
             
            return this.View(viewModel);
        }
    }
}
