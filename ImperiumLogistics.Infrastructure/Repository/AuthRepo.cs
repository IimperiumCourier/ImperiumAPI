using ImperiumLogistics.Domain.AuthAggregate;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository
{
    public class AuthRepo : RepositoryAbstract,IAuthRepo
    {
        private ImperiumDbContext dbContext;
        public AuthRepo(ImperiumDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.User.AddAsync(user);

            return user;
        }

        public Task<User?> GetAsync(Guid id)
        {
            return dbContext.User.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<User?> GetByInfoIdAsync(Guid id)
        {
            return dbContext.User.FirstOrDefaultAsync(e => e.InformationId == id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            dbContext.User.Update(user);

            return user;
        }

        public Task<User?> GetAsync(string email)
        {
            return dbContext.User.FirstOrDefaultAsync(e => e.UserName == email);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
