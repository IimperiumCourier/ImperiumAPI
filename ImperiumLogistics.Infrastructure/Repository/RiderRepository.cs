using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.RiderAggregate;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Repository
{
    public class RiderRepository : RepositoryAbstract, IRiderRepository
    {
        private ImperiumDbContext dbContext;
        public RiderRepository(ImperiumDbContext _context) : base(_context)
        {
            dbContext = _context;
        }

        public Task<Rider> AddRider(AddRiderDto addRiderDto)
        {
            var rider = Rider.GetRider(addRiderDto);

            dbContext.Rider.AddAsync(rider);

            return Task.FromResult(rider);
        }

        public Task<Rider?> GetRider(Guid Id)
        {
            var rider = dbContext.Rider.Where(e => e.Id == Id).FirstOrDefaultAsync();
            return rider;
        }

        public Task<Rider?> GetRider(string emailOrPhoneNum)
        {
            var rider = dbContext.Rider.Where(e => e.Email == emailOrPhoneNum || e.PhoneNumber == emailOrPhoneNum).FirstOrDefaultAsync();
            return rider;
        }

        public Task<bool> IsConnectedToRecord(string emailOrPhoneNum)
        {
            string searchCriteria = emailOrPhoneNum.Trim().ToLower();
            return dbContext.Rider.AnyAsync(e => e.PhoneNumber == searchCriteria ||
                                                   e.Email == searchCriteria);
        }

        public Task<int> Save()
        {
            return dbContext.SaveChangesAsync();
        }

        public void UpdateRider(Rider rider)
        {
            dbContext.Rider.Update(rider);
        }

        public IQueryable<Rider> GetAll()
        {
            return from rider in dbContext.Rider select rider;
        }
    }
}
