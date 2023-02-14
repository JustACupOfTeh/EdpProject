using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCO2Tracker.Models
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        
        [Required]
        public string Email { get; set; }

		[Required, MinLength(12, ErrorMessage = "{0} length can't be less than {1}.")]
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
		public string Password { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PointsCurrent { get; set; }
        public int PointsTotal { get; set; }

        //Relationships
        public List<User_ShopItems>? User_ShopItems { get; set; }
    }
}
