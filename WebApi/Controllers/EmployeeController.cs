using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using BusinessLayer.Model.Models;
using System.Threading.Tasks;
using Ninject.Extensions.Logging;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;

            EmployeeInfo obj = new EmployeeInfo()
            {
                SiteId = "1",
                CompanyCode = "2",
                EmployeeName = "5",
                EmployeeCode = "34"
            };
            _employeeService.SaveEmployee(obj);

            obj = new EmployeeInfo()
            {
                SiteId = "2",
                CompanyCode = "1",
                EmployeeName = "5",
                EmployeeCode = "34"
            };
            _employeeService.SaveEmployee(obj);
        }

        // GET api/<controller>
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var items = await _employeeService.GetAllEmployees();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);              
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception: Grabbing Data for All Employees", e);
                return null;
            }
        }

        [HttpGet]
        [Route("GetByEmployeeCode")]
        public async Task<EmployeeDto> GetEmployeeByCode(string employeeCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCode(employeeCode);
                if (item == null)
                {
                    throw new Exception("The Provided EmployeeCode is not found");
                }
                return _mapper.Map<EmployeeDto>(item);
            }
            catch(Exception e)
            {
                _logger.ErrorException("Exception: Getting Employee by Employee Code", e);
                return null;
            }

        }

        [HttpPut]
        public async Task<bool> Put( [FromBody] EmployeeDto employee)
        {
            if (String.IsNullOrEmpty( employee.EmployeeCode))
            {
                throw new Exception("Employee Code is not valid");
            }
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);

            var result = await _employeeService.GetEmployeeByCode(employee.EmployeeCode); 

            if (result == null)
            {
                throw new Exception("The Provided EmployeeCode is not found");
            }

            return await _employeeService.SaveEmployee(employeeInfo);
        }

        //POST api/<controller>
        [HttpPost]
        public async Task<bool> Post([FromBody] EmployeeDto employee)
        {
            try
            {
                var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
                return await _employeeService.SaveEmployee(employeeInfo);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception: Grabbing Sending Data To Employees", e);
                return false;
            }

        }

        // DELETE api/<controller>
        [HttpDelete]
        [Route("{employeeCode}")]
        public async Task<bool> Delete(string employeeCode)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByCode(employeeCode);

                if (result == null)
                {
                    throw new Exception("The Provided EmployeeCode is not found when trying to Delete in Employee Database");
                }
                return await _employeeService.DeleteEmployee(employeeCode);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception: Deleting Employees", e);
                return false;
            }
        }


    }
}
