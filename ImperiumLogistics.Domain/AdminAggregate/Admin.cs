using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AdminAggregate
{
    public class Admin : Entity<Guid>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public Credential Credential { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryTime { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsSuperAdmin { get; private set; }

        public Admin(Guid id) : base(id)
        {

        }

        public Admin() : base(Guid.NewGuid())
        {

        }

        public static Admin GetAdmin(string name, string email, string phoneNo)
        {
            return new Admin
            {
                Credential = Credential.Add("admin@1234"),
                DateCreated = Utility.GetNigerianTime(),
                Email = Email.Add(email),
                FullName = name.ToSentenceCase(),
                PhoneNumber = phoneNo,
                IsActive = true
            };
        }

        public void UpdatePassword(string password)
        {
            Credential.ChangePassword(password);
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

        public void Delete()
        {
            IsActive = false;
        }
    }
}