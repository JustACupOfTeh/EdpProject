using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
    public class TrackingService
    {

        private readonly MyDbContext _context;

        public TrackingService(MyDbContext context)
        {
            _context = context;
        }

        public void AddTrackingInstance(TrackingCLASS trackinginstance)
        {
            _context.TrackingDB.Add(trackinginstance);
            _context.SaveChanges();
        }

        /*public List<TrackingCLASS> GetAll()
        {
            return _context.TrackingDB.OrderBy(d => d.UserID).ToList();
        }*/



        public TrackingCLASS? GetTrackingById(string id)
        {
            TrackingCLASS? track = _context.TrackingDB.FirstOrDefault(x => x.Id.Equals(id));
            return track;
        }

       


    }
}
