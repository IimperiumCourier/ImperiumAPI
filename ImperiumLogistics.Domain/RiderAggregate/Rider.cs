using ImperiumLogistics.Domain.AuthAggregate;
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
        public bool IsActive { get; private set; }
        public string FrequentLocation { get; private set; }
        public string BikeRegistrationNumber { get; private set; }
        public string LicenseNumber { get; private set; }

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
                FullName= addRiderDto.FullName.ToSentenceCase(),
                IsActive = true,
                FrequentLocation = addRiderDto.FrequentLocation.ToSentenceCase(),
                LicenseNumber= addRiderDto.LicenseNumber.ToUpper(),
                BikeRegistrationNumber = addRiderDto.BikeRegistrationNumber.ToUpper()
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

        public User CreateUser()
        {
            return User.Create(new AddUserDto
            {
                InformationId = Id,
                Name = FullName,
                PhoneNumber = PhoneNumber,
                Password = Utility.DefaultRiderPassword,
                Role = UserRoles.Rider,
                UserName = Email.RemoveSpace()
            });
        }

        public void Update(UpdateRiderDto data)
        {
            if (!string.IsNullOrWhiteSpace(data.PhoneNumber))
            {
                PhoneNumber = data.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(data.FullName))
            {
                FullName = data.FullName.ToSentenceCase();
            }

            if (!string.IsNullOrWhiteSpace(data.FrequentLocation))
            {
                FrequentLocation = data.FrequentLocation.ToSentenceCase();
            }
            
            if(!string.IsNullOrWhiteSpace(data.LicenseNumber)) 
            {
                LicenseNumber = data.LicenseNumber.ToUpper(); 
            }

            if (!string.IsNullOrWhiteSpace(data.BikeRegistrationNumber))
            {
                BikeRegistrationNumber = data.BikeRegistrationNumber.ToUpper();
            }
           
        }
    }
}
