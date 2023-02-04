using eCO2Tracker.Models;
namespace eCO2Tracker.Services
{
    public class UserService
    {
        private readonly MyDbContext _context;
        public UserService(MyDbContext context)
        {
            _context = context;
        }
        public User? GetUserFirst()
        {
            if(_context.Users.First() != null)
            {
                User user = _context.Users.First();
                return user;
            }
            else
            {
                return null;
            }
            
        }

    }
}
