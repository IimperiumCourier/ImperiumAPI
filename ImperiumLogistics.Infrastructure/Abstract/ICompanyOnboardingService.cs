using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface ICompanyService
    {
        Task<ServiceResponse<string>> CreateAccount(CompanyAccountCreationRequest request);
        Task<ServiceResponse<AuthenticationResponse>> CreatePassword(CompanyPasswordCreationRequest request);
    }
}
