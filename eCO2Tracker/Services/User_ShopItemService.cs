using eCO2Tracker.Models;
using Microsoft.EntityFrameworkCore;

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
        public List<User_ShopItems> GetShopItems(string userid)
        {
            return _context.UserItems.Where(x => x.UserID == userid)
                .Include(x => x.ShopItem)
                .ToList();
        }
        public void DeleteAllUserShopItems()
        {
            _context.UserItems.RemoveRange(_context.UserItems.ToList());
            _context.SaveChanges();
        }
    }
}
