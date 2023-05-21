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
            return new Package
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
                Weight = package.WeightOfPackage
            };
        }

        public void SetStatus(PackageStatus status)
        {
            Status = status.GetString();
            LastDateUpdated = Utility.GetNigerianTime();
        }
    }
}
