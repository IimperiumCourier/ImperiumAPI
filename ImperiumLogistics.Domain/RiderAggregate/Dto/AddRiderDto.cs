using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.RiderAggregate.Dto
{
    public class AddRiderDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FrequentLocation { get; set; }
        public string BikeRegistrationNumber { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class GetRiderDto : AddRiderDto { 
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public static GetRiderDto Create(Rider rider)
        {
            return new GetRiderDto
            {
                Email= rider.Email,
                PhoneNumber= rider.PhoneNumber,
                IsActive= rider.IsActive,
                FullName= rider.FullName,
                Id = rider.Id,
                BikeRegistrationNumber = rider.BikeRegistrationNumber,
                LicenseNumber = rider.LicenseNumber,
                FrequentLocation = rider.FrequentLocation
            };
        }
    }
}
