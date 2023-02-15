using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCO2Tracker.Models
{
    public class Lifestyles
    {
        [Key]
        public string EntryID { get; set; } = string.Empty;
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string EntryTitle { get; set; }
        [Display(Name = "Description")]
        public string EntryDesc { get; set; }
        [Display(Name = "Short Description")]
        public string EntryShortDesc { get; set; }
        [Display(Name = "Impact")]
        public string EntryImpact { get; set; }
        [MaxLength(150)]
        [Display(Name = "Reference")]
        public string EntryReferences { get; set; }
        [MaxLength(50)]
        public string? EntryImageURL { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        [Display(Name = "Status")]
        public bool EntryStatus { get; set; } = false;

    }
}