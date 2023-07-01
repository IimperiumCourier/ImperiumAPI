using ImperiumLogistics.Domain.RiderAggregate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.RiderAggregate
{
    public interface IRiderRepository
    {
        public Task<bool> IsConnectedToRecord(string emailOrPhoneNum);
        public Task<Rider?> GetRider(Guid Id);
        public Task<Rider?> GetRider(string emailOrPhoneNum);
        public Task<Rider> AddRider(AddRiderDto addRiderDto);
        public void UpdateRider(Rider rider);
        public Task<int> Save();
        IQueryable<Rider> GetAll();
    }
}
