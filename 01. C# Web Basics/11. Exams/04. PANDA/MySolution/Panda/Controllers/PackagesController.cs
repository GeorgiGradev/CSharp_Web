using Panda.Services.Packages;
using Panda.Services.Users;
using Panda.ViewModels.Packages;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IPackagesService packagesService;

        public PackagesController(
            IUsersService usersService,
            IPackagesService packagesService)
        {
            this.usersService = usersService;
            this.packagesService = packagesService;
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var users = usersService.GetAllUsernames();
            var viewModel = new CreatePackageViewModel
            {
                Usernames = users
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreatePackageInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }


            if (string.IsNullOrEmpty(input.Description)
                    || input.Description.Length < 5
                    || input.Description.Length > 20)
            {
                return this.Redirect("/Packages/Create");
            }

            this.packagesService.CreatePackage(input);

            return this.Redirect("/");
        }

        public HttpResponse Pending()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.packagesService.GetAllPengingPackages();

            return this.View(viewModel);
        }

        public HttpResponse Delivered()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.packagesService.GetAllDeliveredPackages();

            return this.View(viewModel);
        }

        public HttpResponse Deliver(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.packagesService.DeliverPackage(id);

            return this.Redirect("/Packages/Delivered");
        }
    }
}
