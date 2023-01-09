using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
    public class DepartmentService
    {
        private readonly MyDbContext _context;

        public DepartmentService(MyDbContext context)
        {
            _context = context;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.OrderBy(d => d.Name).ToList();
        }

        public Department? GetDepartmentById(string id)
        {
            Department? department = _context.Departments.FirstOrDefault(x => x.DepartmentId.Equals(id));
            return department;
        }
    }
}
