using employee_crud.Data;
using employee_crud.Models;
using employee_crud.Models.Domain;
using Microsoft.AspNetCore.Mvc;

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
                Console.WriteLine($"An exception occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            return RedirectToAction("Add");
        }
    }
}
