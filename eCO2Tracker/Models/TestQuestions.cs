using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eCO2Tracker.Models
{
    public class TestQuestions
    {
        [Key]
        public string QuestionID { get; set; } = string.Empty;
        [MaxLength(100)]
        [Display(Name = "Question Title")]
        public string QuestionTitle { get; set; }
        [Display(Name = "Question Type")]
        public string QuestionType { get; set; }
        [Display(Name = "Question Section")]
        public string QuestionSection { get; set; }
        public string Recommendation { get; set; }
        public bool QuestionStatus { get; set; } = false;
    }
}
