using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace eCO2Tracker.Models
{
    public class TestAnswers
    {
        [Key]
        public string ResponseID { get; set; } = string.Empty;
        public string QuestionID { get; set; }
        public string UserID { get; set; }
        public string Answer { get; set; }
    }
}
