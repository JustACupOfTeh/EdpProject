using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCO2Tracker.Models
{
    public class Lifestyle
    {
        public string entryID { get; set; } = generateUuid();
        [Required, MaxLength(100)]
        [Display(Name = "Title")]
        public string entryTitle { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Description")]
        public string entryDesc { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Impact")]
        public string entryImpact { get; set; } = string.Empty;
        [Required, MaxLength(150)]
        [Display(Name = "Reference")]
        public string entryReferences { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string entryImageURL { get; set; } = string.Empty;
        public DateTime entryDate { get; set; } = DateTime.Now;
        public bool entryStatus { get; set; } = false;

        public static string generateUuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
