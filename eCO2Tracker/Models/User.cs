using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class User
    {
        [Key]
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        [Required, Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? PointsCurrent { get; set; }
        public int? PointsTotal { get; set; }
        public string? Role { get; set; }
        [Display(Name = "Referral Code")]
        public string? ReferralCode { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        //Relationships
        public List<User_ShopItems>? User_ShopItems { get; set; }
    }
}
