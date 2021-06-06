using Panda.Data.Models;
using Panda.Services;
using Panda.ViewModels;
using Panda.ViewModels.Packages;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackagesService packagesService;
        private readonly IUsersService usersService;

        public PackagesController(IPackagesService packagesService, IUsersService usersService)
        {
            this.packagesService = packagesService;
            this.usersService = usersService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var users = this.usersService.GetAllUsernames();
            return this.View(users);
        }


        [HttpPost]
        public HttpResponse Create(CreatePackageInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Description)
                || model.Description.Length < 5
                || model.Description.Length > 20)
            {
                return this.Redirect("/Packages/Create");
            }

            if (string.IsNullOrEmpty(model.RecipientName))
            {
                return this.Redirect("/Packages/Create");
            }

            this.packagesService.CreatePackage(model.Description, model.Weight, model.ShippingAddress, model.RecipientName);

            return this.Redirect("/Packages/Pending");
        }

        public HttpResponse Delivered()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }


            var packages = this.packagesService
                .GetAllByStatus(PackageStatus.Delivered)
                .Select(x => new PackageViewModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    RecipientName = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                }).ToList();

            return this.View(new PackagesListViewModel { Packages = packages });
        }

        public HttpResponse Pending()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }


            var packages = this.packagesService
                .GetAllByStatus(PackageStatus.Pending)
                .Select(x => new PackageViewModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    RecipientName = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                }).ToList();

            return this.View(new PackagesListViewModel { Packages = packages });
        }

        public HttpResponse Deliver(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.packagesService.Deliver(id);

            return this.Redirect("/Packages/Delivered");
        }

    }
}
