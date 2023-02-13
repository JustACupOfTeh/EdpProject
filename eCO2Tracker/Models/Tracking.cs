using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


//TrackingCLASS

namespace eCO2Tracker.Models
{
    public class TrackingCLASS
    {

        [Key]
        [Required]
        public int TrackingDBID { get; set; }


        [Required]
        [Display(Name = "UserID")]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "DistanceInstance")]
        [Column(TypeName = "int")]
        public int DistanceInstance { get; set; }

        [Required]
        [Display(Name = "PointsGained")]
        [Column(TypeName = "int")]
        public int PointsGained { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "TravelDate")]
        [Column(TypeName = "date")]
        public DateTime TravelDate { get; set; } = new DateTime(DateTime.Now.Year - 23, 1, 1);

    }
}
