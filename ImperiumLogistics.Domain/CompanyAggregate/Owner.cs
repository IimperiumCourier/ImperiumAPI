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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static Owner Add(string firstName, string lastName)
        {
            return new Owner { FirstName = firstName, LastName = lastName };
        }
    }
}
