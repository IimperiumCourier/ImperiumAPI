using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ImperiumLogistics.Infrastructure.Repository
{
    public class AdminRepository : RepositoryAbstract, IAdminRepository
    {
        private ImperiumDbContext dbContext;
        public AdminRepository(ImperiumDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Admin> Add(string phoneNo, string fullName, string emailAddress)
        {
            var admin = Admin.GetAdmin(fullName,emailAddress, phoneNo);

            dbContext.Admin.AddAsync(admin);

            return Task.FromResult(admin);
        }

        public IQueryable<Admin> GetAll()
        {
            return from admin in dbContext.Admin select admin;
        }

        public Task<Admin?> GetByEmail(string email)
        {
            return dbContext.Admin.FirstOrDefaultAsync(e => e.Email.Address == email);
        }

        public Task<Admin?> GetById(Guid id)
        {
            return dbContext.Admin.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Update(Admin admin)
        {
            dbContext.Admin.Update(admin);
        }
    }
}
