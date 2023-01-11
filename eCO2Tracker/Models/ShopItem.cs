using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eCO2Tracker.Models
{
    public enum ItemType
    {
        PMD, 
        Other
    }
    public class ShopItem
    {

        [Key]
        public string ItemID { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public float ItemPrice { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public ItemType ItemType { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime ExpiredDate { get; set; } 

    }
}
