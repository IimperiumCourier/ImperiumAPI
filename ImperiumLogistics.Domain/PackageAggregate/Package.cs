using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using ImperiumLogistics.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public class Package : Entity<Guid>
    {
        public Guid PlacedBy { get; private set; }
        public PackageAddress DeliveryAddress { get; private set; }
        public PackageAddress PickUpAddress { get; private set; }
        public PackageCusomer Cusomer { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public DateTime LastDateUpdated { get; private set; }
        public string TrackingNumber { get; private set; }
        public int NumberOfItems { get; private set; }
        public decimal Weight { get; private set; }
        public string QRCode { get; private set; }
        public DateTime ExpectedDeliveryDate { get; private set; }
        public AssignedRider PickupRider { get; private set; }
        public AssignedRider DeliveryRider { get; private set; }

        public Package(Guid id): base(id)
        {
        }

        public Package() : base(Guid.NewGuid())
        {
            DateCreated = Utility.GetNigerianTime();
            LastDateUpdated = DateTime.MinValue;
        }

        public static Package GetPackage(PackageDto package)
        {
            string deliveryCity = package.DeliveryCity.ToSentenceCase();
            string pickUpCity = package.PickUpCity.ToSentenceCase();

            var _package = new Package
            {
                Cusomer = PackageCusomer.GetCusomer(package.CustomerFirstName.ToSentenceCase(),
                                                    package.CustomerLastName.ToSentenceCase(),
                                                    package.CustomerPhoneNumber.ConvertToElevenDigits()),

                DeliveryAddress = PackageAddress.GetAddress(package.DeliveryAddress.ToSentenceCase(),
                                                            package.DeliveryCity.ToSentenceCase(),
                                                            package.DeliveryState.ToSentenceCase(),
                                                            package.DeliveryLandMark.ToSentenceCase()),

                PickUpAddress = PackageAddress.GetAddress(package.PickUpAddress.ToSentenceCase(),
                                                            package.PickUpCity.ToSentenceCase(),
                                                            package.PickUpState.ToSentenceCase(),
                                                            package.PickUpLandMark.ToSentenceCase()),

                Description = package.PackageDescription.ToSentenceCase(),
                Status = PackageStatus.AvailableForPickUp.GetString(),
                PlacedBy = package.PackagePlacedBy,
                TrackingNumber = TrackingNumberGenerator.GenerateTrackingNumber(),
                NumberOfItems = package.NumberOfItems,
                Weight = package.WeightOfPackage,
                ExpectedDeliveryDate = pickUpCity.Contains("Lagos") && deliveryCity.Contains("Lagos") 
                                        ? Utility.GetNigerianTime().AddDays(3) : Utility.GetNigerianTime().AddDays(7)
            };

            string _serializedString = package.ToJson();
            _package.QRCode = Utility.CompressToBase64(Utility.GenerateQRCode(_serializedString));

            return _package;
        }

        public void SetStatus(PackageStatus status)
        {
            Status = status.GetString();
            LastDateUpdated = Utility.GetNigerianTime();
        }

        public void GenerateQRCode(string url)
        {
            string _uri = $"{url}{TrackingNumber}";
            QRCode = Utility.CompressToBase64(Utility.GenerateQRCode(_uri));
        }

        public string GetQRCode()
        {
            return Utility.DecompressFromBase64(QRCode);
        }

        public void AssignPickupRider(Guid riderId, Guid assignedBy)
        {
            PickupRider = AssignedRider.GetRider(riderId, assignedBy);
        }

        public void AssignDeliveryRider(Guid riderId, Guid assignedBy)
        {
            DeliveryRider = AssignedRider.GetRider(riderId, assignedBy);
        }
    }
}
