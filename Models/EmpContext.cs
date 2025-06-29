using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace EmployManagment.Models
{
    public class EmpContext : IdentityDbContext<ApplicationUser>
    {
        public EmpContext() : base()
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR", Description = "Human Resources" },
                new Department { Id = 2, Name = "IT", Description = "Information Technology" },
                new Department { Id = 3, Name = "Finance", Description = "Finance Department" }
            );

            // Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    name = "Ahmed Ali",
                    email = "ahmed.ali@example.com",
                    phone = "01000000001",
                    salary = 5000,
                    address = "Cairo",
                    departmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    name = "Sara Hassan",
                    email = "sara.hassan@example.com",
                    phone = "01000000002",
                    salary = 6000,
                    address = "Giza",
                    departmentId = 2
                },
                new Employee
                {
                    Id = 3,
                    name = "Mohamed Youssef",
                    email = "mohamed.youssef@example.com",
                    phone = "01000000003",
                    salary = 7000,
                    address = "Alexandria",
                    departmentId = 2
                }
            );
        }

        public EmpContext(DbContextOptions<EmpContext> options) : base(options)
        {
        }
    }
    

    
}
