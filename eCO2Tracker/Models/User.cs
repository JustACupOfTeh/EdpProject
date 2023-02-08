using Microsoft.OpenApi.Any;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCO2Tracker.Models
{
    public class User
    {
        public User() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, Column("First_Name")]
        public string? FirstName { get; set; }

        [Required, Column("Last_Name")]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required, MinLength(12, ErrorMessage = "{0} length can't be less than {1}.")]
        public string? Password { get; set; }

        public string Role { get; set; }

        [Column("Referral_Code")]
        public string? ReferralCode { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        public List<User_ShopItems>? User_ShopItems { get; set; }


    }
}
