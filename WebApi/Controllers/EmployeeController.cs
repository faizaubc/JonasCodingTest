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
                _logger.ErrorException("Exception", e);
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
                _logger.ErrorException("Exception", e);
                return false;
            }

        }

   
    }
}
