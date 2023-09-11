using employee_crud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace employee_crud.Data
{
    public class EmployeeCrudDBContext : DbContext
    {
        public EmployeeCrudDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
