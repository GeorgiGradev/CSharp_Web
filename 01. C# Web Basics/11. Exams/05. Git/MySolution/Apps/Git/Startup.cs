namespace Git
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using Git.Services.Users;
    using Git.Services.Commits;
    using Git.Services.Repositories;

    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ICommitsService, CommitsService>();
            serviceCollection.Add<IRepositoriesService, RepositoriesService>();
        }
    }
}
