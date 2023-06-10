using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate.DTO
{
    public class PackageDto
    {
        public int NumberOfItems { get; set; }
        public decimal WeightOfPackage { get; set; }
        [JsonIgnore]
        public Guid PackagePlacedBy { get; set; }
        public string PackageDescription { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string DeliveryCity { get; set; } = string.Empty;
        public string DeliveryState { get; set; } = string.Empty;
        public string DeliveryLandMark { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string CustomerPhoneNumber { get; set; } = string.Empty;
        public string PickUpAddress { get; set; } = string.Empty;
        public string PickUpCity { get; set; } = string.Empty;
        public string PickUpState { get; set; } = string.Empty;
        public string PickUpLandMark { get; set; } = string.Empty;
    }
}
