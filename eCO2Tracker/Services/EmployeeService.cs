using eCO2Tracker.Models;

namespace eCO2Tracker.Services
{
    public class EmployeeService
    {
        private readonly MyDbContext _context;

        public EmployeeService(MyDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.OrderBy(m => m.Name).ToList();
        }

        public Employee? GetEmployeeById(string id)
        {
            Employee? employee = _context.Employees.FirstOrDefault(x => x.EmployeeId.Equals(id));
            return employee;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }
    }
}
