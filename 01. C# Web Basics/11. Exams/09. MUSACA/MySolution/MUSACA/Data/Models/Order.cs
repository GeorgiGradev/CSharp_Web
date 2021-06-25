using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<ProductOrder>();
        }

        public string Id { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Active;

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ProductOrder> Products { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
