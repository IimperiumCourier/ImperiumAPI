using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public class PackageAddress: ValueObject<PackageAddress>
    {
        public string Address { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string LandMark { get; private set; } = string.Empty;

        internal static PackageAddress GetAddress(string address, string city, string state, string landMark)
        {
            return new PackageAddress
            {
                Address = address,
                City = city,
                State = state,
                LandMark = landMark
            };
        }
    }
}
