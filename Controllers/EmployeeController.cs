using Microsoft.AspNetCore.Mvc;

namespace employee_crud.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
