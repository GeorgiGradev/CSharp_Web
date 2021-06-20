using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.Services.Users;
using BattleCards.ViewModels.Cards;
using BattleCards.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace BattleCards.Services.Cards
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;
        private readonly IUsersService usersService;

        public CardsService(ApplicationDbContext db, IUsersService usersService)
        {
            this.db = db;
            this.usersService = usersService;
        }

        public void CreateCard(CreateCardInputModel model, string userId)
        {
            var card = new Card
            {
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword,
                Description = model.Description,
                Attack = model.Attack,
                Health = model.Health
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();

            System.Console.WriteLine($"userId ={userId}");

            this.db.UserCards.Add(new UserCard
            {
                CardId = card.Id,
                UserId = userId
            });
            this.db.SaveChanges();
                
        }

        public List<CardViewModel> GetAll()
        {
            var viewModel = this.db.Cards
                .Select(x => new CardViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Keyword = x.Keyword,
                    Description = x.Description,
                    Attack = x.Attack,
                    Health = x.Health
                }).ToList();

            return viewModel;
        }

        public MyCollectionViewModel GetMyCollection(string userId)
        {
            var viewModel = this.db.Cards
                .Where(x => x.UserCards.Any(y => y.UserId == userId))
                .Select(x => new MyCardViewModel
                {
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Name = x.Name,
                    Keyword = x.Keyword,
                    Attack = x.Attack,
                    Health = x.Health,
                    CardId = x.Id
                })
                .ToList();

            return new MyCollectionViewModel { MyCollection = viewModel };
        }

        public bool AddToCollection(int cardId, string userId)
        {
            var currentCard = this.db
                .Cards
                .FirstOrDefault(x => x.Id == cardId);

            var currentUser = this.db
                .Users
                .FirstOrDefault(x => x.Id == userId);

            bool isCardExisting = this.db.UserCards.Any(x => x.CardId == currentCard.Id && x.UserId == currentUser.Id);

            if (isCardExisting)
            {
                return true;
            }

            this.db.UserCards.Add(new UserCard
            {
                Card = currentCard,
                User = currentUser
            });
            this.db.SaveChanges();
            return false;
        }

        public void RemoveCard(int cardId, string userId)
        {
            var currentCard = this.db
                .UserCards
                .FirstOrDefault(x => x.CardId == cardId && x.UserId == userId);

            this.db.UserCards.Remove(currentCard);
            this.db.SaveChanges();
        }
    }
}
