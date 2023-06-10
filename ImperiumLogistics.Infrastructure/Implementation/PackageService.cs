using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Mapper;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
using ImperiumLogistics.SharedKernel.Query;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<ServiceResponse<string>> CreatePackage(PackageDto package)
        {
            var response = await _packageRepository.Add(package);
            var dbResponse = await _packageRepository.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            return ServiceResponse<string>.Success($"Package was saved successfully. You can track your package using {response.TrackingNumber}.");
        }

        public ServiceResponse<PagedQueryResult<PackageQueryResponse>> GetAllPackages(PackageQueryRequestDTO queryRequest)
        {
            PagedQueryResult<PackageQueryResponse> _result = new PagedQueryResult<PackageQueryResponse>();
            if (queryRequest != null)
            {

                IQueryable<Package> response = _packageRepository.GetAllByCompanyID(queryRequest.ComanyID);

                var packageFilters = HandlerFactory.GetPackageFilters();
                packageFilters.Apply(ref response, queryRequest);

                int pageSize = queryRequest.PagedQuery != null ? queryRequest.PagedQuery.PageSize : Utility.DefaultPageSize;
                int pageNumber = queryRequest.PagedQuery != null ? queryRequest.PagedQuery.PageSize : Utility.DefaultPageSize;

                var result = response.ToPagedResult(pageNumber, pageSize);

                if (result.TotalItemCount <= 0)
                {
                    return ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Success(new PagedQueryResult<PackageQueryResponse>
                    { Items = new List<PackageQueryResponse>() }, "There were no packages found.");
                }

                var _data = result.Items.Select(e => PackageResponseMapper.GetPackageQueryResponse(e)).ToList();

                _result.Items = _data;
                _result.TotalItemCount = result.TotalItemCount;
                _result.CurrentPageNumber = result.CurrentPageNumber;
                _result.CurrentPageSize = result.CurrentPageSize;
                _result.TotalPageCount = result.TotalPageCount;
                _result.HasPrevious = result.HasPrevious;
                _result.HasNext = result.HasNext;

                return ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Success(_result);

            }
            else
            {
                return ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is invalid");
            }
        }

        public async Task<ServiceResponse<PackageQueryResponse>> GetPackage(string trackingNumber)
        {
            var query = await _packageRepository.GetByTrackNumber(trackingNumber);

            if(query is null)
            {
                return ServiceResponse<PackageQueryResponse>.Error($"There is no package with tracking number {trackingNumber} .");
            }

            var response = PackageResponseMapper.GetPackageQueryResponse(query);

            return ServiceResponse<PackageQueryResponse>.Success(response);
        }

        public async Task<ServiceResponse<PackageQueryResponse>> GetPackage(Guid packageID)
        {
            var query = await _packageRepository.GetById(packageID);

            if (query is null)
            {
                return ServiceResponse<PackageQueryResponse>.Error($"There is no package with ID {packageID} .");
            }

            var response = PackageResponseMapper.GetPackageQueryResponse(query);

            return ServiceResponse<PackageQueryResponse>.Success(response);
        }

        public async Task<ServiceResponse<string>> UpdatePackageStatus(Guid id, PackageStatus packageStatus)
        {
            var query = await _packageRepository.GetById(id);
            if (query is null)
            {
                return ServiceResponse<string>.Error($"There is no package with ID {id} .");
            }

            query.SetStatus(packageStatus);
            _packageRepository.Update(query);

            var dbResponse = await _packageRepository.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while updating package status.");
            }

            return ServiceResponse<string>.Success($"Package was updated successfully.");
        }

        public async Task<ServiceResponse<string>> UpdatePackageStatus(string trackingNumber, PackageStatus packageStatus)
        {
            var query = await _packageRepository.GetByTrackNumber(trackingNumber);
            if (query is null)
            {
                return ServiceResponse<string>.Error($"There is no package with tracking number {trackingNumber} .");
            }

            query.SetStatus(packageStatus);
            _packageRepository.Update(query);

            var dbResponse = await _packageRepository.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while updating package status.");
            }

            return ServiceResponse<string>.Success($"Package was updated successfully.");
        }
    }
}
