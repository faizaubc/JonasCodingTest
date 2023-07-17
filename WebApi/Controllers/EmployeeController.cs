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


        //POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeDto employee)
        {
            try
            {
                EmployeeInfo employeeInfo = _mapper.Map<EmployeeInfo>(employee);
                return await _employeeService.SaveEmployee(employeeInfo);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception: Grabbing Sending Data To Employees", e);
                return false;
            }

        }

   
    }
}
