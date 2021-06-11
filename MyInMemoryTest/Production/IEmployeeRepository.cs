using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyInMemoryTest.Production.Entities;

namespace MyInMemoryTest.Production
{
    public interface IEmployeeRepository
    {
        Task AddAsync(EmployeeDto employeeDto);

        Task<IList<Employee>> FindByDepartmentId(DepartmentEnum id);
    }
}