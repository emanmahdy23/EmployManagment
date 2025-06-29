using Microsoft.AspNetCore.Mvc;
using EmployManagment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace EmployManagment.Controllers
{
    [Authorize]
    public class EmpController : Controller
    {
        private readonly EmpContext _context;

        public EmpController(EmpContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            
            var employees = _context.Employees
                                    .Include(e => e.Department)
                                    .ToList();

            return View("index",employees);
        }

        public IActionResult Create()
        {
            var viewModel = new EmployeeViewModel
            {
                Departments = _context.Departments.ToList()
            };

            return View("create",viewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                viewModel.Departments = _context.Departments.ToList();
                return View("create",viewModel);
            }

            var employee = new Employee
            {
                name = viewModel.name,
                email = viewModel.email,
                phone = viewModel.phone,
                salary = viewModel.salary,
                address = viewModel.address,
                departmentId = viewModel.departmentId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

       
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                name = employee.name,
                email = employee.email,
                phone = employee.phone,
                salary = employee.salary,
                address = employee.address,
                departmentId = employee.departmentId,
                Departments = _context.Departments.ToList()
            };

            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                viewModel.Departments = _context.Departments.ToList();
                return View(viewModel);
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            employee.name = viewModel.name;
            employee.email = viewModel.email;
            employee.phone = viewModel.phone;
            employee.salary = viewModel.salary;
            employee.address = viewModel.address;
            employee.departmentId = viewModel.departmentId;

            _context.Update(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                name = employee.name,
                email = employee.email,
                phone = employee.phone,
                salary = employee.salary,
                address = employee.address,
                departmentName = employee.Department?.Name
            };

            return View("Delete",viewModel);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}
