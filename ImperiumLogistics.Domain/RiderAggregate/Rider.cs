using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.RiderAggregate
{
    public class Rider : Entity<Guid>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime DateCreated { get; private set; }
        public bool IsActive { get; private set; }
        public Credential Credential { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryTime { get; private set; }

        public Rider(Guid id) : base(id)
        {

        }

        public Rider() : base(Guid.NewGuid())
        {

        }

        public static Rider GetRider(AddRiderDto addRiderDto)
        {
            return new Rider
            {
                DateCreated = Utility.GetNigerianTime(),
                Email = addRiderDto.Email.Trim().ToLower(),
                PhoneNumber = addRiderDto.PhoneNumber,
                FullName= addRiderDto.FullName,
                IsActive = true,
                Credential = Credential.Add("rider@1234")
            };
        }

        public void DeactivateRider()
        {
            IsActive = false;
        }

        public void ActivateRider()
        {
            IsActive = true;
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
    }
}
