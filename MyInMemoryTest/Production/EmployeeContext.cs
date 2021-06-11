using Microsoft.EntityFrameworkCore;
using MyInMemoryTest.Production.Entities;

namespace MyInMemoryTest.Production
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            
        }
        public virtual DbSet<Employee> Employees { get; set; } 


    }
}