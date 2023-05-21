using ImperiumLogistics.Domain.PackageAggregate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class PackageQueryResponse : PackageDto
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }
    }
}
