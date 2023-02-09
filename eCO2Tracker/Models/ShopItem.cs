using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eCO2Tracker.Models
{
    public class ShopItem
    {

        [Key]
        public string ItemID { get; set; } = string.Empty;
        [Display(Name = "Item")]
        public string ItemName { get; set; } = string.Empty;
        [Display(Name = "Description")]
        public string ItemDescription { get; set; } = string.Empty;
        public string ItemDescriptionSummary { get; set; } = string.Empty;
        [Display(Name = "Quantity")]
        [Range(0, 999, ErrorMessage = "Quantity must be an integer and cannot be less than 0 or more than 999")]
        public int ItemCount { get; set; }
        [Display(Name = "Cost")]
        [Range(0, 10000, ErrorMessage = "Cost must be an integer and cannot be less than 0 or more than 10000")]
        public int ItemPrice { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        [Display(Name = "Type")]
        public string ItemType { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ExpiresAt { get; set; }

        //Relationships
        public virtual List<User_ShopItems>? User_ShopItems { get; set; }

    }
}
