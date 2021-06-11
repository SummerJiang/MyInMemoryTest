using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyInMemoryTest.Production.Entities;

namespace MyInMemoryTest.Production
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="employeeDto">傳入的員工資料</param>
        public async Task AddAsync(EmployeeDto employeeDto)
        {
            // 假設已經驗證資料不存在資料庫，直接新增。
            var employee = new Employee()
            {
                Name = $"{employeeDto.FirstName} {employeeDto.LastName}",
                DepartmentId = (int)employeeDto.Department
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 依據單位代碼取得該單位所有員工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IList<Employee>> FindByDepartmentId(DepartmentEnum id)
        {
            return await _context.Employees.Where(s => s.DepartmentId == (int) id).ToListAsync();
        }
    }

    public class EmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DepartmentEnum Department { get; set; }
    }

    public enum DepartmentEnum
    {
        海外企業開發組 = 1,
        火箭隊與她快樂夥伴 = 2
    }
}