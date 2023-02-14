using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class UserView
    {
        public string? Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
