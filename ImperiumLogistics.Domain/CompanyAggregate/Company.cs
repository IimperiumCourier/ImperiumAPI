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
        public Credential Credential { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryTime { get; private set;}

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

        public void AddPassword(string password)
        {
            if(Credential is null)
            {
                EmailVerified();
            }

            Credential = Credential.Add(password);
        }

        public bool HasNotSetPassword()
        {
            return ReferenceEquals(null, Credential);
        }

        public void UpdatePassword(string password) 
        {
            Credential.ChangePassword(password);
        }

        public void EmailVerified()
        {
            EmailAddress.VerificationSucceded();
        }

        public void AddRefreshToken(string refreshToken, DateTime duration)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = duration;
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public bool HasNoRefreshToken()
        {
            return ReferenceEquals(null, RefreshToken);
        }

        public bool HasExpiredRefreshToken()
        {
            return RefreshTokenExpiryTime <= Utility.GetNigerianTime();
        }
    }
}
