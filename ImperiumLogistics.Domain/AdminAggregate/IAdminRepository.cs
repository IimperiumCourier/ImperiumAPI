using ImperiumLogistics.Domain.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AdminAggregate
{
    public interface IAdminRepository
    {
        IQueryable<Admin> GetAll();
        Task<Admin?> GetById(Guid id);
        Task<Admin?> GetByEmail(string email);
        Task<Admin> Add(string phoneNo, string fullName, string emailAddress);
        void Update(Admin admin);
        Task<int> Save();
    }
}
