namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
