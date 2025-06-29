namespace EmployManagment.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
       
        // Navigation property for related employees
        public List<Employee> Employees { get; set; }
    }
}
