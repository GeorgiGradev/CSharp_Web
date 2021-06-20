namespace BattleCards
{
    using System.Collections.Generic;
    using BattleCards.Data;
    using BattleCards.Services.Cards;
    using BattleCards.Services.Users;
    using Microsoft.EntityFrameworkCore;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<ICardsService, CardsService>();
            serviceCollection.Add<IUsersService, UsersService>();
        }
    }
}
