using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface IRiderService
    {
        Task<ServiceResponse<GetRiderDto>> GetRider(Guid id);
        Task<ServiceResponse<string>> AddRider(AddRiderDto rider);
        ServiceResponse<PagedQueryResult<GetRiderDto>> GetAllRiders(QueryRequest request);
    }
}
