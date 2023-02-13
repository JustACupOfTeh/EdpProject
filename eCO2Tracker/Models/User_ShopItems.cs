namespace eCO2Tracker.Models
{
    public class User_ShopItems
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string ItemID { get; set; }
        public ShopItem ShopItem { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}
