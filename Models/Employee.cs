namespace EmployManagment.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public double salary { get; set; }

        public string? address { get; set; }
       public int departmentId { get; set; }
        // Navigation property for related department
        public Department? Department { get; set; }
       


    }
}
