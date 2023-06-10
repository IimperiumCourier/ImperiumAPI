using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Mapper
{
    public class PackageResponseMapper
    {
        public static PackageQueryResponse GetPackageQueryResponse(Package package)
        {
            return new PackageQueryResponse
            {
                CustomerFirstName = package.Cusomer.FirstName,
                CustomerLastName = package.Cusomer.LastName,
                CustomerPhoneNumber = package.Cusomer.PhoneNumber,
                DeliveryAddress = package.DeliveryAddress.Address,
                DeliveryCity = package.DeliveryAddress.City,
                DeliveryLandMark = package.DeliveryAddress.LandMark,
                DeliveryState = package.DeliveryAddress.State,
                PackageDescription = package.Description,
                PackagePlacedBy = package.PlacedBy,
                PickUpAddress = package.PickUpAddress.Address,
                PickUpCity = package.PickUpAddress.City,
                PickUpLandMark = package.PickUpAddress.LandMark,
                PickUpState = package.PickUpAddress.State,
                TrackingNumber = package.TrackingNumber,
                Id = package.Id,
                NumberOfItems = package.NumberOfItems,
                WeightOfPackage = package.Weight,
                QRCode = package.GetQRCode()
            };
        }

        public static PackageQueryResponse GetPackageQueryResponseV2(Package package)
        {
            return new PackageQueryResponse
            {
                CustomerFirstName = package.Cusomer.FirstName,
                CustomerLastName = package.Cusomer.LastName,
                CustomerPhoneNumber = package.Cusomer.PhoneNumber,
                DeliveryAddress = package.DeliveryAddress.Address,
                DeliveryCity = package.DeliveryAddress.City,
                DeliveryLandMark = package.DeliveryAddress.LandMark,
                DeliveryState = package.DeliveryAddress.State,
                PackageDescription = package.Description,
                PackagePlacedBy = package.PlacedBy,
                PickUpAddress = package.PickUpAddress.Address,
                PickUpCity = package.PickUpAddress.City,
                PickUpLandMark = package.PickUpAddress.LandMark,
                PickUpState = package.PickUpAddress.State,
                TrackingNumber = package.TrackingNumber,
                Id = package.Id,
                NumberOfItems = package.NumberOfItems,
                WeightOfPackage = package.Weight
            };
        }
    }
}
