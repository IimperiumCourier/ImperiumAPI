using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AuthAggregate
{
    public interface IAuthRepo
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User?> GetAsync(Guid id);
        Task<User?> GetAsync(string email);
        Task<User?> GetByInfoIdAsync(Guid id);
        Task<int> Save();

    }
}
