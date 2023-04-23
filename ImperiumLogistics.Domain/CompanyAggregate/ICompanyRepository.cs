using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.CompanyAggregate
{
    public interface ICompanyRepository
    {
        IQueryable<Company> GetAll();
        Task<Company?> GetById(Guid id);
        Task<Company?> GetByEmail(string email);
        Task<Company> Add(string phoneNo, string houseAddress, string city,
                                 string state, string fullName, string companyName,
                                 string emailAddress);
        void Update(Company company);
        Task<int> Save();
        Task<bool> HasCompanyAccount(string searchCriteria);
    }
}
