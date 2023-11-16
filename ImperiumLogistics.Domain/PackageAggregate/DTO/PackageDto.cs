using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "A description of the package is required.")]
        public string PackageDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Delivery address is required.")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Delivery city is required.")]
        public string DeliveryCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Delivery state is required.")]
        public string DeliveryState { get; set; } = string.Empty;
        public string DeliveryLandMark { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer firstname is required.")]
        public string CustomerFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer lastname is required.")]
        public string CustomerLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer phonenumber is required.")]
        public string CustomerPhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pick up address is required.")]
        public string PickUpAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pick up city is required.")]
        public string PickUpCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pick up state is required.")]
        public string PickUpState { get; set; } = string.Empty;
        public string PickUpLandMark { get; set; } = string.Empty;
        public Guid DeliveryRider { get; set; } = Guid.Empty;
        public Guid PickUpRider { get; set; } = Guid.Empty;
    }
}
