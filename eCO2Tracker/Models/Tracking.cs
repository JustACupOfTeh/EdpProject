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
        public string UserID { get; set; }

        public int DistanceInstance { get; set; }


        [DataType(DataType.Date)]
        public DateTime TravelDate { get; set; } = new DateTime(DateTime.Now.Year - 23, 1, 1);

    }
}
