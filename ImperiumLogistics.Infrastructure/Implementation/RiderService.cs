using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    internal class RiderService : IRiderService
    {
        public Task<ServiceResponse<string>> AddRider(AddRiderDto rider)
        {
            throw new NotImplementedException();
        }

        public Task<PagedQueryResult<GetRiderDto>> GetAllRiders(QueryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetRiderDto>> GetRider(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
