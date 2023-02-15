using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
    public class LifestyleService
    {
        private readonly MyDbContext _context;

        public LifestyleService(MyDbContext context)
        {
            _context = context;
        }

        public List<Lifestyles> GetAll()
        {
            return _context.Lifestyle.OrderBy(m => m.EntryDate).ToList();
        }

        public Lifestyles? GetLifestyleById(string id)
        {
            Lifestyles? lifestyle = _context.Lifestyle.FirstOrDefault(x => x.EntryID.Equals(id));
            return lifestyle;
        }

        public void AddLifestyle(Lifestyles lifestyle)
        {
            _context.Lifestyle.Add(lifestyle);
            _context.SaveChanges();
        }

        public void UpdateLifestyle(Lifestyles lifestyle)
        {
            _context.Lifestyle.Update(lifestyle);
            _context.SaveChanges();
        }

        public void RemoveLifestyle(Lifestyles lifestyle)
        {
            _context.Lifestyle.Remove(lifestyle);
            _context.SaveChanges();
        }

        public string generateUuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
