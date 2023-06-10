using ImperiumLogistics.Domain.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.UserRetrievalHandler
{
    internal class CompanyUser : UserHandler
    {
        public override async Task GetUserByUsername(string username)
        {
            if(companyRepository != null)
            {
                User = await companyRepository.GetByEmail(username);
                if (User == null && successor != null)
                {
                    await successor.GetUserByUsername(username);
                }
                
                if(User != null)
                {
                    UserType = typeof(Company);
                }
            }
        }
    }
}
