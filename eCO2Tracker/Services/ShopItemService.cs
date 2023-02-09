using eCO2Tracker.Models;
namespace eCO2Tracker.Services
{
    public class ShopItemService
    {
        private readonly MyDbContext _context;
        public ShopItemService(MyDbContext context)
        {
            _context = context;
        }
        public List<ShopItem> GetAll()
        {
            return _context.ShopItems
                .OrderBy(m => m.CreatedDate)
                .ToList();
        }
        public ShopItem? GetShopItemById(string id)
        {
            ShopItem? shopItem = _context.ShopItems.FirstOrDefault(x => x.ItemID.Equals(id));
            return shopItem;
        }
        public List<ShopItem> GetShopItemByType(string type)
        {
            return _context.ShopItems.Where(m => m.ItemType == type).ToList();
        }
        public void AddShopItem(ShopItem item)
        {
            DateTime Now = DateTime.Now;
            item.CreatedDate = Now;
            //Fill ItemDescriptionSummary
            if (item.ItemDescription.Length > 20)
            {
                var descSummary = item.ItemDescription.Substring(0, 20) + " ...";
                item.ItemDescriptionSummary = descSummary;
            } 
            else
            {
                item.ItemDescriptionSummary = item.ItemDescription;
            }

            _context.ShopItems.Add(item);
            _context.SaveChanges();
        }
        public void UpdateShopItem(ShopItem item)
        {
            _context.ShopItems.Update(item);
            _context.SaveChanges();
        }
        public void DeleteShopItem(string id)
        {
            ShopItem item = _context.ShopItems.FirstOrDefault(si => si.ItemID == id);
            _context.ShopItems.Remove(item);
            _context.SaveChanges();
        }
        public void BuyShopItem(ShopItem item, User user)
        {
            item.ItemCount -= 1;
            user.PointsCurrent -= item.ItemPrice;
            _context.ShopItems.Update(item);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

    }
}
