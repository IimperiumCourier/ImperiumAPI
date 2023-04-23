using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.CompanyAggregate
{
    public class Owner : ValueObject<Owner>
    {
        public string FullName { get; private set; }

        public static Owner Add(string fullName)
        {
            return new Owner { FullName = fullName.ToSentenceCase() };
        }
    }
}
