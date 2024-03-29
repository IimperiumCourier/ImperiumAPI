﻿using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface IPackageService
    {
        Task<ServiceResponse<PackageCreationRes>> CreatePackage(PackageDto package);
        Task<ServiceResponse<PackageQueryResponse>> GetPackage(string trackingNumber);
        Task<ServiceResponse<PackageQueryResponse>> GetPackage(Guid packageID);
        Task<ServiceResponse<string>> UpdatePackageStatus(Guid id, PackageStatus packageStatus);
        ServiceResponse<PagedQueryResult<PackageQueryResponse>> GetAllPackages(PackageQueryRequestDTO queryRequest);
        Task<ServiceResponse<string>> UpdatePackageStatus(string trackingNumber, PackageStatus packageStatus);
        Task<ServiceResponse<string>> AssignRiderToPackage(PackageAssignRequest packageAssignRequest, Guid adminId);
        ServiceResponse<PagedQueryResult<PackageQueryResponse>> GetAllPackageAssignedToRider(RiderPackageQueryRequest queryRequest);
        ServiceResponse<PagedQueryResult<PackageQueryResponse>> GetAllPackagesInSystem(PackageQueryRequestDTO queryRequest);
        Task<ServiceResponse<PackageAnalytics>> GetPackageAnalytics();
        Task<ServiceResponse<RiderAnalytics>> GetRiderAnalytics(Guid riderId);
    }
}
