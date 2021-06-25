using Microsoft.EntityFrameworkCore;
using MUSACA.Data;
using MUSACA.Services;
using MUSACA.Services.Home;
using MUSACA.Services.Orders;
using MUSACA.Services.Products;
using MUSACA.Services.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;


namespace MUSACA
{
    class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IHomeService, HomeService>();
            serviceCollection.Add<IProductsService, ProductsService>();
            serviceCollection.Add<IOrdersService, OrdersService>();
        }
    }
}
