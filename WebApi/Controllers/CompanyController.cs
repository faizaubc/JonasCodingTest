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
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;

        }
        // GET api/<controller>
        [Route("GetAll")]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception", e);
                return null;
            }
        }

        // GET api/<controller>/5
        [Route("GetByCompanyCode")]
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception", e);
                return null;
            }

        }

        //POST api/<controller>
        public async Task<bool> Post([FromBody] CompanyDto company)
        {
            try
            {
                CompanyInfo companyInfo = _mapper.Map<CompanyInfo>(company);
                return await _companyService.SaveCompany(companyInfo);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception", e);
                return false;
            }
        }

        // PUT api/<controller>/5
        public async Task<bool> Put([FromBody] CompanyDto company)
        {
            CompanyInfo companyInfo = _mapper.Map<CompanyInfo>(company);

            if (String.IsNullOrEmpty(companyInfo.CompanyCode))
            {
                throw new Exception("Invalid Company Code Provided");
            }

            var searchedCompanyInfoObject = await _companyService.GetCompanyByCode(company.CompanyCode);

            if (searchedCompanyInfoObject == null)
            {
                throw new Exception("Provided Company Code not Found");
            }

            try
            {
                return await _companyService.SaveCompany(companyInfo);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception", e);
                return false;
            }

        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            try
            {
                return await _companyService.RemoveCompanyByCode(companyCode);
            }
            catch (Exception e)
            {
                _logger.ErrorException("Exception", e);
                return false;
            }
        }
    }
}