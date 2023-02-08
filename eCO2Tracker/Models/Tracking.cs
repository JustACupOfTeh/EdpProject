using System.ComponentModel.DataAnnotations;


namespace eCO2Tracker.Models
{
    public class Tracking
    {

        [Key]
        public string UserID { get; set; }

        public int DistanceInstance { get; set; }

        public int TravelDate { get; set; }
    }
}
