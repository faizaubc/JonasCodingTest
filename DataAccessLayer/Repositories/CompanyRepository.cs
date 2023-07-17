using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            var result = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));

            return result?.FirstOrDefault();
        }

        public async Task<bool> SaveCompany(Company company)
        {
            var itemRepo = await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode));
            var item = itemRepo?.FirstOrDefault();

            if (item != null)
            {
                item.CompanyName = company.CompanyName;
                item.AddressLine1 = company.AddressLine1;
                item.AddressLine2 = company.AddressLine2;
                item.AddressLine3 = company.AddressLine3;
                item.Country = company.Country;
                item.EquipmentCompanyCode = company.EquipmentCompanyCode;
                item.FaxNumber = company.FaxNumber;
                item.PhoneNumber = company.PhoneNumber;
                item.PostalZipCode = company.PostalZipCode;
                item.LastModified = System.DateTime.Now;
                return await _companyDbWrapper.UpdateAsync(item);
            }
            company.LastModified = System.DateTime.Now;
            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<bool> RemoveByCode(string companyCode)
        {
            bool result = false;
            if (companyCode != null)
            {
                result = await _companyDbWrapper.DeleteAsync(t => t.CompanyCode.Equals(companyCode));
            }
            return result;
        }
    }
}
