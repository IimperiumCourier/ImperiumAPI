using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.UserRetrievalHandler
{
    public abstract class UserHandler
    {
        public Type? UserType { get; set; }
        public object? User { get; protected set; }
        public ICompanyRepository? companyRepository;
        protected UserHandler successor;

        public void SetSuccessor(UserHandler successor)
        {
            this.successor = successor;
        }
        public abstract Task GetUserByUsername(string username);
        
    }
}
