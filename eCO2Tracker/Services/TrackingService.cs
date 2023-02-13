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
        
        //function to add Tracking to TrackinigDB
        public void AddTrackingInstance(TrackingCLASS trackinginstance)
        {
            _context.TrackingDB.Add(trackinginstance);
            _context.SaveChanges();
        }

     

        //puts all items in TrackingDB to a list, will be used in TrackingHistory.cshtml
        public List<TrackingCLASS> GetTrackingList()
        {
            return _context.TrackingDB
                .OrderBy(d => d.TrackingDBID)
                .ToList();
        }

        //does that ^^ but sorts by date
        public List<TrackingCLASS> GetTrackingListByDate()
        {
            return _context.TrackingDB
                .OrderBy(d => d.TravelDate)
                .ToList();
        }

        //does that ^^ but sorts by distance travelled
        public List<TrackingCLASS> GetTrackingByDistanceTravelled()
        {
            return _context.TrackingDB
                .OrderBy(d => d.DistanceInstance)
                .ToList();
        }

        //does that ^^ but sorts by distance travelled
        public List<TrackingCLASS> GetTrackingByPointsGained()
        {
            return _context.TrackingDB
                .OrderBy(d => d.PointsGained)
                .ToList();
        }


        // used in Details page, makes sure that 
        public TrackingCLASS? GetTrackingById(int id)
        {
            TrackingCLASS? track = _context.TrackingDB.FirstOrDefault(x => x.TrackingDBID.Equals(id));
            return track;
        }

        //Delete function
        public void DeleteTrackingInstance(TrackingCLASS trackinginstance)
        {
            _context.TrackingDB.Remove(trackinginstance);
            _context.SaveChanges();
        }


    }
}
