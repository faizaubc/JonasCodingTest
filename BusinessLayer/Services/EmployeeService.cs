using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            if (String.IsNullOrEmpty(employeeCode))
            {
                throw new Exception("Employee Code is not valid");
            }
            var result = await _employeeRepository.GetByCode(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<bool> SaveEmployee(EmployeeInfo employee)
        {
            var employee_ = _mapper.Map<Employee>(employee);
            return await _employeeRepository.SaveEmployee(employee_);
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            if (String.IsNullOrEmpty(employeeCode))
            {
                throw new Exception("Employee Code is not valid");
            }
            return await _employeeRepository.DeleteEmployee(employeeCode);
            
        }
    }
}
