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

    }
}
