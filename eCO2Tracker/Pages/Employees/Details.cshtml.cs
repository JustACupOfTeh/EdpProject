using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;
        private IWebHostEnvironment _environment;

        public DetailsModel(EmployeeService employeeService, DepartmentService departmentService, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _environment = environment;
        }

        [BindProperty]
        public Employee MyEmployee { get; set; } = new();

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public static List<Department> DepartmentList { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            DepartmentList = _departmentService.GetAll();

            Employee? employee = _employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                MyEmployee = employee;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Employee ID {0} not found", id);
                return Redirect("/Employees");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    if (MyEmployee.ImageURL != null)
                    {
                        var oldImageFile = Path.GetFileName(MyEmployee.ImageURL);
                        var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyEmployee.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _employeeService.UpdateEmployee(MyEmployee);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Employee {0} is updated", MyEmployee.Name);
            }
            return Page();
        }
    }
}
