using SUS.HTTP;
using MishMash.Data;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MishMash.Services;
using MishMash.Services.Home;
using MishMash.Services.Channels;

namespace MishMash
{
    public class StartUp : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IHomeService, HomeService>();
            serviceCollection.Add<IChannelsService, ChannelsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
