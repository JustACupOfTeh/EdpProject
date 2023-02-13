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
        public User? GetUserById(string id)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserID.Equals(id));
            return user;
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

        //Check for prescence of specified user. Adds to user points by total amount specified. Returns true or false
        public bool AddUserPointsBy(string userid, int pts)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserID.Equals(userid));
            if(user != null) 
            {

                user.PointsCurrent = user.PointsCurrent + pts;
                user.PointsTotal = user.PointsTotal + pts;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        //Check for prescence of specified user. Removes from user points by total amount specified. Returns true or false
        public bool DeleteUserPointsBy(string userid, int pts) 
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserID.Equals(userid));
            if (user != null)
            {
                user.PointsCurrent = user.PointsCurrent - pts;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
