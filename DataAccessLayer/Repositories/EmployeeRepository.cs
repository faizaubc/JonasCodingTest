using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var employeeRepo = await _employeeDbWrapper.FindAsync(t =>
                t.CompanyCode.Equals(employee.CompanyCode));
            var item = employeeRepo?.FirstOrDefault();

            if (item != null)
            {
                item.EmployeeCode = employee.EmployeeCode;
                item.EmployeeName = employee.EmployeeName;
                item.Occupation = employee.Occupation;
                item.EmployeeStatus = employee.EmployeeStatus;
                item.EmailAddress = employee.EmailAddress;
                item.Phone = employee.Phone;
                item.LastModified = employee.LastModified;

                return await _employeeDbWrapper.UpdateAsync(item);
            }

            return await _employeeDbWrapper.InsertAsync(employee);
        }
    }
}
