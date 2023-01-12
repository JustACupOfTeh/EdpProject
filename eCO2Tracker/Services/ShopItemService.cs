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
            return _context.ShopItems.OrderBy(m => m.ItemName).ToList();
        }
        public ShopItem? GetShopItemById(string id)
        {
            ShopItem? shopItem = _context.ShopItems.FirstOrDefault(x => x.ItemID.Equals(id));
            return shopItem;
        }
        public void AddShopItem(ShopItem item)
        {
            _context.ShopItems.Add(item);
            _context.SaveChanges();
        }
        public void UpdaterShopItem(ShopItem item)
        {
            _context.ShopItems.Update(item);
            _context.SaveChanges();
        }
        public void DeleteShopItem(ShopItem item)
        {
            _context.ShopItems.Remove(item);
            _context.SaveChanges();
        }

    }
}
