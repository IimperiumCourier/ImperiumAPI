using Azure;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
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

        public ServiceResponse<List<PackageQueryResponse>> GetAllPackages(PackageQueryRequest queryRequest)
        {
            IQueryable<Package> response;
            if(!queryRequest.UseDateForFilter())
            {
                return ServiceResponse<List<PackageQueryResponse>>.Error("An error occurred while retrieving all packages.");
            }
            else
            {
                response = _packageRepository.GetAll(queryRequest.From, queryRequest.To, queryRequest.ComanyID);
            }

            var _data = response.Select(e => new PackageQueryResponse
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

            return ServiceResponse<List<PackageQueryResponse>>.Success(_data);
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
                WeightOfPackage = query.Weight
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
                NumberOfItems = query.NumberOfItems
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
