namespace SharedTrip.Data
{
   public class UserTrip
    {

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public string TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
