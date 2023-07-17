using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> SaveEmployee(EmployeeInfo employeeInfo);
        Task<EmployeeInfo> GetEmployeeByCode(string employeeCode);

        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();
        Task<bool> DeleteEmployee(string employeeCode);
    }
}
