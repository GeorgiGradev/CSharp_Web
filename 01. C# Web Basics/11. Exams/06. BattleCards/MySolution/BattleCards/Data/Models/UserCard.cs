using System;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data.Models
{
    public class UserCard
    {
        public UserCard()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CardId { get; set; }

        public virtual Card Card { get; set; }
    }
}
