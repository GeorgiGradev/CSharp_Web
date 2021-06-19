using Panda.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Data.Models
{
    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }

        [Range(0,10000000000)]
        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }
    }
}
