using System.ComponentModel;
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
        [Display(Name = "Item")]
        public string ItemName { get; set; } = string.Empty;
        [Display(Name = "Description")]
        public string ItemDescription { get; set; } = string.Empty;
        [Display(Name = "Quantity")]
        public int ItemCount { get; set; }
        [Display(Name = "Cost")]
        public int ItemPrice { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public ItemType ItemType { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime ExpiredDate { get; set; } 

    }
}
