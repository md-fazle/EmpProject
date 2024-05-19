using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext dbContext;
        public EmployeesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult  Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                EmployeeId = viewModel.EmployeeId,
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Gender = viewModel.Gender,
                Birthday = viewModel.Birthday,
                Department = viewModel.Department,
                Salary = viewModel.Salary,
                Address = viewModel.Address

            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Employees");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return View(employees);

        }

        public async Task<IActionResult> List(String searchString)
        {
            var employees = dbContext.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString) || x.Department.Contains(searchString));
            }
            var filteredEmployees = await dbContext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var emplyee = await dbContext.Employees.FindAsync(id);          
            return View(emplyee);
        }

        [HttpPost]

        public async Task<IActionResult>Edit(Employee viewModel)
        {
           var emplyee = await dbContext.Employees.FindAsync(viewModel.id);          
            if (emplyee != null)
            {
                emplyee.EmployeeId = viewModel.EmployeeId;
                emplyee.Name = viewModel.Name;
                emplyee.Email= viewModel.Email;
                emplyee.Phone = viewModel.Phone;
                emplyee.Gender = viewModel.Gender;
                emplyee.Birthday = viewModel.Birthday;
                emplyee.Department= viewModel.Department;
                emplyee.Salary = viewModel.Salary;
                emplyee.Address = viewModel.Address;

                await dbContext .SaveChangesAsync();
            }
            return RedirectToAction("List","Employees");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.id == viewModel.id);

            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employees");
        }


    }
}
