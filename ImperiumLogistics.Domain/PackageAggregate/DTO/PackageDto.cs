using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate.DTO
{
    public class PackageDto
    {
        public Guid PackagePlacedBy { get; set; }
        public string PackageDescription { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryLandMark { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string PickUpAddress { get; set; }
        public string PickUpCity { get; set; }
        public string PickUpState { get;set; }
        public string PickUpLandMark { get;set; }
    }
}
