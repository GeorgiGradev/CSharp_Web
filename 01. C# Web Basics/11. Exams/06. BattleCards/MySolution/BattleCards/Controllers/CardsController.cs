using BattleCards.Services.Cards;
using BattleCards.Services.Users;
using BattleCards.ViewModels.Cards;
using BattleCards.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;
        private readonly IUsersService usersService;

        public CardsController(ICardsService cardsService, IUsersService usersService)
        {
            this.cardsService = cardsService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.cardsService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateCardInputModel model, LoginInputModel login)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(model.Name)
              || model.Name.Length < 5
              || model.Name.Length > 15)
            {
                return this.Error("Invalid card name");
            }

            if (string.IsNullOrEmpty(model.Image))
            {
                return this.Error("Invalid image url");
            }

            if (string.IsNullOrEmpty(model.Keyword))
            {
                return this.Error("Invalid keyword");
            }

            if (model.Attack < 0)
            {
                return this.Error("Attack can not be negative integer");
            }

            if (model.Health < 0)
            {
                return this.Error("Health can not be negative integer");
            }

            if (string.IsNullOrEmpty(model.Description)
                || model.Description.Length > 200)
            {
                return this.Error("Invalid description");
            }

            var userId = this.GetUserId();

            this.cardsService.CreateCard(model, userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Collection(string id)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            var viewModel = this.cardsService.GetMyCollection(userId);
            return this.View(viewModel);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            string userId = this.GetUserId(); // да се върне в метода

            bool isCardExisting = this.cardsService.AddToCollection(cardId, userId);

            if (isCardExisting)
            {
                return this.Redirect("/Cards/All");
            }

            return this.Redirect("/Cards/Collection");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            this.cardsService.RemoveCard(cardId, userId);

            return this.Redirect("/Cards/Collection");
        }
    }
}
