using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Suls.Services.Problems;
using Suls.Services.Submissions;
using Suls.Services.Users;
using SulsApp.Data;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemsService>();
            serviceCollection.Add<ISubmissionsService, SubmissionsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
