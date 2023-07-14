using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using BusinessLayer.Model.Models;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [Route("GetAll")]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        [Route("GetByCompanyCode")]
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        //POST api/<controller>
        public async Task<bool> Post([FromBody] CompanyDto company)
        {
            CompanyInfo companyInfo = _mapper.Map<CompanyInfo>(company);
            return await _companyService.SaveCompany(companyInfo);
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

            return await _companyService.SaveCompany(companyInfo);

        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string companyCode)
        {
            return await _companyService.RemoveCompanyByCode(companyCode);
        }
    }
}