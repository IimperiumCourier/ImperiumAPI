﻿using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
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
        Task<ServiceResponse<BusinessAnalytics>> GetAnalytics(Guid companyId);
        ServiceResponse<PagedQueryResult<Company>> GetAllCompanies(QueryRequest queryRequest);
        Task<ServiceResponse<Company>> UpdateAccount(CompanyAccountUpdateRequest request);
        Task<ServiceResponse<Company>> GetCompanyInformation(Guid companyId);
    }
}
