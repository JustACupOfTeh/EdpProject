namespace eCO2Tracker.Models
{
    public class UserShopItems
    {
        public string UserID { get; set; }
        public User User { get; set; }
        public string ItemID { get; set; }
        public ShopItem ShopItem { get; set; }

    }
}
