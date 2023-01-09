using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeService _employeeService;

        public IndexModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public List<Employee> EmployeeList { get; set; } = new();

        public void OnGet()
        {
            EmployeeList = _employeeService.GetAll();
        }
    }
}
