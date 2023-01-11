using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eCO2Tracker.Models
{
    public class ShopItem
    {
        public string ItemID { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public float ItemPrice { get; set; }
        public string Image { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime ExpiredDate { get; set; } 

    }
}
