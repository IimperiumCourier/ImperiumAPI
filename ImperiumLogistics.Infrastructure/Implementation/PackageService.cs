using Azure;
using Humanizer;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.Infrastructure.PackageHandlers;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IQueryable<Package> response = _packageRepository.GetAllByCompanyID(queryRequest.ComanyID);

            var packageFilters = HandlerFactory.GetPackageFilters();
            packageFilters.Apply(response, queryRequest);

            var result = response.ToPagedResult(queryRequest.PagedQuery.PageNumber, queryRequest.PagedQuery.PageSize);

            if(result.TotalItemCount <= 0)
            {
                return ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Success(new PagedQueryResult<PackageQueryResponse> 
                { Items = new List<PackageQueryResponse>() }, "There were no packages found.");
            }

            var _data = result.Items.Select(e => new PackageQueryResponse
            {
                CustomerFirstName = e.Cusomer.FirstName,
                CustomerLastName = e.Cusomer.LastName,
                CustomerPhoneNumber = e.Cusomer.PhoneNumber,
                DeliveryAddress = e.DeliveryAddress.Address,
                DeliveryCity = e.DeliveryAddress.City,
                DeliveryLandMark = e.DeliveryAddress.LandMark,
                DeliveryState = e.DeliveryAddress.State,
                PackageDescription = e.Description,
                PackagePlacedBy = e.PlacedBy,
                PickUpAddress = e.PickUpAddress.Address,
                PickUpCity = e.PickUpAddress.City,
                PickUpLandMark = e.PickUpAddress.LandMark,
                PickUpState = e.PickUpAddress.State,
                TrackingNumber = e.TrackingNumber,
                Id = e.Id,
                NumberOfItems = e.NumberOfItems,
                WeightOfPackage = e.Weight
            }).ToList();

            _result.Items = _data;
            _result.TotalItemCount = result.TotalItemCount;
            _result.CurrentPageNumber = result.CurrentPageNumber;
            _result.CurrentPageSize = result.CurrentPageSize;
            _result.TotalPageCount = result.TotalPageCount;
            _result.HasPrevious = result.HasPrevious;
            _result.HasNext = result.HasNext;

            return ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Success(_result);
        }

        public async Task<ServiceResponse<PackageQueryResponse>> GetPackage(string trackingNumber)
        {
            var query = await _packageRepository.GetByTrackNumber(trackingNumber);

            if(query is null)
            {
                return ServiceResponse<PackageQueryResponse>.Error($"There is no package with tracking number {trackingNumber} .");
            }

            var response = new PackageQueryResponse
            {
                CustomerFirstName = query.Cusomer.FirstName,
                CustomerLastName = query.Cusomer.LastName,
                CustomerPhoneNumber = query.Cusomer.PhoneNumber,
                DeliveryAddress = query.DeliveryAddress.Address,
                DeliveryCity = query.DeliveryAddress.City,
                DeliveryLandMark = query.DeliveryAddress.LandMark,
                DeliveryState = query.DeliveryAddress.State,
                PackageDescription = query.Description,
                PackagePlacedBy = query.PlacedBy,
                PickUpAddress = query.PickUpAddress.Address,
                PickUpCity = query.PickUpAddress.City,
                PickUpLandMark = query.PickUpAddress.LandMark,
                PickUpState = query.PickUpAddress.State,
                TrackingNumber = query.TrackingNumber,
                Id = query.Id,
                NumberOfItems = query.NumberOfItems,
                WeightOfPackage = query.Weight,
                PackageStatus = query.Status,
                QRCode = query.QRCode
            };

            return ServiceResponse<PackageQueryResponse>.Success(response);
        }

        public async Task<ServiceResponse<PackageQueryResponse>> GetPackage(Guid packageID)
        {
            var query = await _packageRepository.GetById(packageID);

            if (query is null)
            {
                return ServiceResponse<PackageQueryResponse>.Error($"There is no package with ID {packageID} .");
            }

            var response = new PackageQueryResponse
            {
                CustomerFirstName = query.Cusomer.FirstName,
                CustomerLastName = query.Cusomer.LastName,
                CustomerPhoneNumber = query.Cusomer.PhoneNumber,
                DeliveryAddress = query.DeliveryAddress.Address,
                DeliveryCity = query.DeliveryAddress.City,
                DeliveryLandMark = query.DeliveryAddress.LandMark,
                DeliveryState = query.DeliveryAddress.State,
                PackageDescription = query.Description,
                PackagePlacedBy = query.PlacedBy,
                PickUpAddress = query.PickUpAddress.Address,
                PickUpCity = query.PickUpAddress.City,
                PickUpLandMark = query.PickUpAddress.LandMark,
                PickUpState = query.PickUpAddress.State,
                TrackingNumber = query.TrackingNumber,
                Id = query.Id,
                WeightOfPackage = query.Weight,
                NumberOfItems = query.NumberOfItems,
                PackageStatus = query.Status,
                QRCode = query.QRCode
            };

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
