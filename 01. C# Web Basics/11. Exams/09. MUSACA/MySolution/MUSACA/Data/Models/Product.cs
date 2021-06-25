using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<ProductOrder>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
