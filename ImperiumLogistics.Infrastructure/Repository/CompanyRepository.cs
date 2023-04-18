using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository
{
    public class CompanyRepository : RepositoryAbstract,ICompanyRepository
    {
        private ImperiumDbContext dbContext;
        public CompanyRepository(ImperiumDbContext dbContext) :base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Company> Add(string phoneNo, string houseAddress, string city,
                                 string state, string firstName, string lastName,
                                 string emailAddress)
        {
            var company = Company.Create(phoneNo,houseAddress,city, state, firstName, lastName, emailAddress);
            dbContext.Company.AddAsync(company);

            return Task.FromResult(company);
        }

        public IQueryable<Company> GetAll()
        {
            return from company in dbContext.Company select company;
        }

        public Task<Company?> GetByEmail(string email)
        {
            return dbContext.Company.FirstOrDefaultAsync(e => e.EmailAddress.Address == email);
        }

        public Task<Company?> GetById(Guid id)
        {
            return dbContext.Company.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<bool> HasCompanyAccount(string searchCriteria)
        {
            return dbContext.Company.AnyAsync(e => e.PhoneNumber == searchCriteria ||
                                                   e.EmailAddress.Address == searchCriteria);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Update(Company company)
        {
            dbContext.Company.Update(company);
        }
    }
}
