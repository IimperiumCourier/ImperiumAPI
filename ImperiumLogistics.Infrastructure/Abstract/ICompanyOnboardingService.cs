using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface ICompanyOnboardingService
    {
        Task<ServiceResponse<string>> CreateAccount(CompanyAccountCreationRequest request);
        Task<ServiceResponse<string>> CreatePassword(CompanyPasswordCreationRequest request);
    }
}
