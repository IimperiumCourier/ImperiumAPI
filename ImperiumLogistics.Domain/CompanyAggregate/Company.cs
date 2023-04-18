using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.CompanyAggregate
{
    public class Company : Entity<Guid>
    {
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Email EmailAddress { get; private set; }
        public Owner Owner { get; private set; }
        public Credential Credential { get; private set; }

        public Company(Guid Id):base(Id)
        {
        }

        public Company():base(Guid.NewGuid()) 
        {
            DateCreated = Utility.GetNigerianTime();
        }

        public static Company Create(string phoneNo, string houseAddress, string city,
                                     string state, string firstName, string lastName,
                                     string emailAddress)
        {
            return new Company
            {
                City = city,
                State = state,
                Address = houseAddress,
                EmailAddress = Email.Add(emailAddress),
                PhoneNumber = phoneNo,
                Owner = Owner.Add(firstName, lastName)
            };
        }

        public void AddPassword(string password)
        {
            Credential = Credential.Add(password);
        }

        public void UpdatePassword(string password) 
        {
            Credential.ChangePassword(password);
        }

        public void EmailVerified()
        {
            EmailAddress.VerificationSucceded();
        }
    }
}
