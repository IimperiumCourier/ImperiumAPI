using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate
{
    public class AssignedRider : ValueObject<AssignedRider>
    {
        public Guid AssignedBy { get; private set; }
        public Guid RiderId { get; private set; }
        public DateTime DateAssigned { get; private set; }


        public static AssignedRider GetRider(Guid riderId, Guid assignedBy)
        {
            return new AssignedRider
            {
                DateAssigned = Utility.GetNigerianTime(),
                RiderId = riderId,
                AssignedBy = assignedBy
            };
        }
    }
}
