using System.ComponentModel.DataAnnotations;

namespace MishMash.Data
{
    public class ChannelTag
    {
        public int Id { get; set; }

        [Required]
        public string ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        [Required]
        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
