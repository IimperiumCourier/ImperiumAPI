using ImperiumLogistics.Domain.CompanyAggregate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.UserRetrievalHandler
{
    public class UserRetrievalFactory
    {
        private ICompanyRepository companyRepository;
        public UserRetrievalFactory(ICompanyRepository companyRepo)
        { 
            companyRepository = companyRepo;
        }
        public UserHandler GetUser()
        {
            CompanyUser companyUser = new CompanyUser();
            companyUser.companyRepository = companyRepository;

            return companyUser;
        }
    }
}
