using ImperiumLogistics.Domain.AuthAggregate;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using ImperiumLogistics.SharedKernel.Enums;
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
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Email EmailAddress { get; private set; }
        public Owner Owner { get; private set; }

        public Company(Guid Id):base(Id)
        {
        }

        public Company():base(Guid.NewGuid()) 
        {
            DateCreated = Utility.GetNigerianTime();
        }

        public static Company Create(string phoneNo, string houseAddress, string city,
                                     string state, string fullName, string companyName,
                                     string emailAddress)
        {
            return new Company
            {
                City = city.ToSentenceCase(),
                State = state.ToSentenceCase(),
                Address = houseAddress.ToSentenceCase(),
                EmailAddress = Email.Add(emailAddress),
                PhoneNumber = phoneNo,
                Owner = Owner.Add(fullName),
                Name = companyName.ToSentenceCase()
            };
        }        

        public void EmailVerified()
        {
            EmailAddress.VerificationSucceded();
        }

        public User CreateUser()
        {
            return User.Create(new AddUserDto
            {
                InformationId = Id,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Password = Utility.DefaultRiderPassword,
                Role = UserRoles.Company,
                UserName = EmailAddress.Address.RemoveSpace()
            });
        }
    }
}
