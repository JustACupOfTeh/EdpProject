using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ReferralCode { get; set; }
        public int PointsCurrent { get; set; }
        public int PointsTotal { get; set; }

        //Relationships
        public List<User_ShopItems>? User_ShopItems { get; set; }
    }
}
