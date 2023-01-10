using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eCO2Tracker.Models
{
    public class ShopItem
    {
        [Column(TypeName = "int")]
        public int itemId { get; set; }
        public string itemName { get; set; }
        public float price { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "date")]
        [Display(Name ="Expiry date")]
        public DateTime DateExpire { get; set; }
        public int itemCount { get; set; }

    }
}
