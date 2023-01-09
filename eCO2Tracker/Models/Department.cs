using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class Department
    {
        [Required, MinLength(2), MaxLength(8)]
        public string DepartmentId { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        public List<Employee>? Employees { get; set; }
    }
}
