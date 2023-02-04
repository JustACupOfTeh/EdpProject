using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCO2Tracker.Models
{
    public class Lifestyles
    {
        [Key]
        public string EntryID { get; set; } = generateUuid();
        [Required, MaxLength(100)]
        [Display(Name = "Title")]
        public string EntryTitle { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Description")]
        public string EntryDesc { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Impact")]
        public string EntryImpact { get; set; } = string.Empty;
        [Required, MaxLength(150)]
        [Display(Name = "Reference")]
        public string EntryReferences { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string EntryImageURL { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public bool EntryStatus { get; set; } = false;

        public static string generateUuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
