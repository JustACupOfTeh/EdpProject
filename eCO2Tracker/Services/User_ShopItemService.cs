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
    }
}
