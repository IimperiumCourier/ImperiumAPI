using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AuthAggregate
{
    public class User : Entity<Guid>
    {
        public Guid InformationId { get; private set; }
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public Credential Credential { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryTime { get; private set; }
        public string Role { get; private set; }
        public bool IsActive { get; private set; }

        public User(Guid Id):base(Id)
        {

        }

        public User() : base(Guid.NewGuid())
        {

        }

        public static User Create(AddUserDto user)
        {
            return new User
            {
                UserName = user.UserName.RemoveSpace(),
                InformationId = user.InformationId,
                Credential = Credential.Add(user.Password),
                Role = user.Role,
                IsActive = true,
                DateCreated = Utility.GetNigerianTime(),
                PhoneNumber =  user.PhoneNumber,
                Name = user.Name
            };
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

        public void UpdatePassword(string password)
        {
            Credential.ChangePassword(password);
        }
    }
}
