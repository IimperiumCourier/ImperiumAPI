using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public class PackageCusomer : ValueObject<PackageCusomer>
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        internal static PackageCusomer GetCusomer(string firstName, string lastName, string phoneNo)
        {
            return new PackageCusomer { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNo };
        }
    }
}
