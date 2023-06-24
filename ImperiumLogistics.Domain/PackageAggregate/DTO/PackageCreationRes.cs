using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.PackageAggregate.DTO
{
    public class PackageCreationRes
    {
        public string TrackingNumber { get; set; }
        public string CreatedBy { get; set; }
        public string QRCode { get; set; }

        public static PackageCreationRes CreationResponse(string trackingNum, string createdBy, string QRCode)
        {
            return new PackageCreationRes
            {
                CreatedBy = createdBy,
                TrackingNumber = trackingNum,
                QRCode = QRCode
            };
        }
    }
}
