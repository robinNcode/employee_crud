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
        public async  Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
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

            await employeeCrudDBContext.AddAsync(employeeInfo);
            await employeeCrudDBContext.SaveChangesAsync();

            return RedirectToAction("Add");
        }
    }
}
