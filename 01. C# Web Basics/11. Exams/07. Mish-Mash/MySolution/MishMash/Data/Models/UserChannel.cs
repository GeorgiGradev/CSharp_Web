using System.ComponentModel.DataAnnotations;

namespace MishMash.Data
{
    public class UserChannel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
