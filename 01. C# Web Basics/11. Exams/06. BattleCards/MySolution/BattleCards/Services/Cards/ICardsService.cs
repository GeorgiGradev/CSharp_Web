using BattleCards.ViewModels.Cards;
using System.Collections.Generic;

namespace BattleCards.Services.Cards
{
    public interface ICardsService
    {
        public void CreateCard(CreateCardInputModel model, string userId);

        public List<CardViewModel> GetAll();

        public MyCollectionViewModel GetMyCollection(string userId);

        public bool AddToCollection(int cardId, string userId);

        public void RemoveCard(int cardId, string userId);
    }
}
