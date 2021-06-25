using System.ComponentModel.DataAnnotations;

namespace MUSACA.Data.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        [Required]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}