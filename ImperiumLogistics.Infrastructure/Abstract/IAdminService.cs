using ImperiumLogistics.Domain.AdminAggregate.Dto;
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
    public interface IAdminService
    {
        Task<ServiceResponse<string>> CreateAdmin(AdminCreationRequest request);
        Task<ServiceResponse<string>> ChangePassword(string email, string password);
        Task<ServiceResponse<string>> DeleteAdmin(string email, string requestedBy);
        ServiceResponse<PagedQueryResult<AdminDto>> GetAllAdmin(QueryRequest queryRequest);
    }
}
