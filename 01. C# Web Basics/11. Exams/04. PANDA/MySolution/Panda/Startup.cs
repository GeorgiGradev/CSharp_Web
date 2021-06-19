using SUS.HTTP;
using Panda.Data;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Panda.Services.Users;
using Panda.Services.Packages;
using Panda.Services.Receipts;

namespace Panda
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IPackagesService, PackageService>();
            serviceCollection.Add<IReceiptsService, ReceiptsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}