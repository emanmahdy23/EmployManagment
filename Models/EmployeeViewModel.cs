using System.ComponentModel.DataAnnotations;

namespace EmployManagment.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? phone { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public double salary { get; set; }

        public string? address { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int departmentId { get; set; }
        public string? departmentName { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}

