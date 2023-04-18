using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.CompanyAggregate
{
    public class Email : ValueObject<Email>
    {
        public string Address { get; private set; } = string.Empty;
        public bool IsVerified { get; private set; } = false;

        internal static Email Add(string emailAddress)
        {
            return new Email { Address = emailAddress };
        }
        
        public void VerificationSucceded()
        {
            IsVerified = true;
        }
    }
}
