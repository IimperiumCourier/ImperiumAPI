using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public class PackageDescription : Entity<Guid>
    {
        public string Name { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime DateUpdated { get; private set; }

        public PackageDescription(Guid Id) : base(Id)
        {

        }

        public PackageDescription() : base(Guid.NewGuid())
        {
            DateCreated = Utility.GetNigerianTime();
            DateUpdated = DateTime.MinValue;
        }

        public static PackageDescription CreatePackageDescription(string name)
        {
            return new PackageDescription
            {
                Name = name,
                IsDeleted = false
            };
        }

        public void Deleted()
        {
            IsDeleted = true;
            DateUpdated = Utility.GetNigerianTime();
        }
    }
}
