using eCO2Tracker.Models;
using eCO2Tracker.Utility;
using Microsoft.EntityFrameworkCore;


namespace eCO2Tracker.Services
{
    public class UserService
    {
        private readonly MyDbContext _context;
        public UserService(MyDbContext context)
        {
            _context = context;
        }
        public List<User> GetAll()
        {
            return _context.User.OrderBy(d => d.Email).ToList();
        }
       
        public User? Login(User loginUser)
        {
            User? user = _context.User.FirstOrDefault(
            x => x.Email == loginUser.Email);

            if(user != null)
            {
                if (Hashing.ValidatePassword(loginUser.Password, user.Password) == true)
                {
                    return user;
                }
            }
            
            return null;
        }

        public User? GetUserById(string id)
        {
            User? user = _context.User.FirstOrDefault(
            x => x.Id.ToString() == id);
            return user;
        }

        public string GetReferralCodeById(string id)
        {
            User? user = _context.User.FirstOrDefault(
            x => x.Id.ToString() == id);
            return user.ReferralCode;
        }

        public void CreateUser(User newUser)
        {
            if (CheckIfUserExist(newUser.Email) != true)
            {
                _context.User.Add(newUser);
                _context.SaveChanges();
            }
        }

        public bool CheckIfUserExist(string? email)
        {
            bool status = true;

            User? user = _context.User.FirstOrDefault(x => x.Email == email);

            //user does not exist 
            if (user == null)
            {
                status = false;
            }

            return status;
        }

        public void UpdateUser(User userToBeUpdated)
        {
            using (_context)
            {
                User? user = _context.User.FirstOrDefault(x => x.Id.Equals(userToBeUpdated.Id));

                if (user != null)
                {
                    user.FirstName = userToBeUpdated.FirstName;
                    user.LastName = userToBeUpdated.LastName;
                    user.Password = userToBeUpdated.Password;
                    user.UpdatedDate = userToBeUpdated.UpdatedDate;

                    _context.User.Update(user);
                    _context.SaveChanges();
                }
            }

        }

        public void DeleteUser(User user)
        { 
            _context.User.Remove(user);
            _context.SaveChanges();
        }

    }
}
