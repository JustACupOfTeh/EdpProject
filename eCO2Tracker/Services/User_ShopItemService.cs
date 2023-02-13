using eCO2Tracker.Models;
namespace eCO2Tracker.Services
{
    public class User_ShopItemService
    {
        private readonly MyDbContext _context;
        public User_ShopItemService(MyDbContext context)
        {
            _context = context;
        }
        public void BuyShopItem(User user, ShopItem shopitem)
        {
            string id = Guid.NewGuid().ToString();
            DateTime deliverdate = DateTime.Now.AddDays(3);

            User_ShopItems usershopitems = new User_ShopItems { Id = id, User = user, ShopItem = shopitem, DeliveryDate = deliverdate};
            _context.UserItems.Add(usershopitems);
            _context.SaveChanges();
        }
    }
}
