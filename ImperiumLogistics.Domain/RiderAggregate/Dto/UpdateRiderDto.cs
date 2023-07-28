using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.RiderAggregate.Dto
{
    public class UpdateRiderDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string FrequentLocation { get; set; }
        public string BikeRegistrationNumber { get; set; }
        public string LicenseNumber { get; set; }
    }
}
