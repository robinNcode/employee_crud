using employee_crud.Data;
using employee_crud.Models;
using employee_crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace employee_crud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeCrudDBContext employeeCrudDBContext;
        public EmployeeController(EmployeeCrudDBContext employeeCrudDBContext)
        {
            this.employeeCrudDBContext = employeeCrudDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeCrudDBContext.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employeeInfo = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            try
            {
                await employeeCrudDBContext.AddAsync(employeeInfo);
                await employeeCrudDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                //Console.WriteLine($"An exception occurred: {ex.Message}");
                //Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await employeeCrudDBContext.Employees.FirstOrDefaultAsync(emp => emp.Id == id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel requestInfo)
        {
            try
            {
                // Getting specific employee info by requested employee id ...
                var employee = await employeeCrudDBContext.Employees.FindAsync(requestInfo.Id);
                
                if (employee != null)
                {
                    employee.Name = requestInfo.Name;
                    employee.Email = requestInfo.Email;
                    employee.Salary = requestInfo.Salary;
                    employee.Department = requestInfo.Department;
                    employee.DateOfBirth = requestInfo.DateOfBirth;

                    await employeeCrudDBContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("Failed to get employee Data!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel requestInfo)
        {
            // Getting specific employee info by requested employee id ...
            var employee = await employeeCrudDBContext.Employees.FindAsync(requestInfo.Id);

            if (employee != null)
            {
                employeeCrudDBContext.Employees.Remove(employee);
                await employeeCrudDBContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
