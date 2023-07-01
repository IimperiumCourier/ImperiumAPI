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
    }

    public class GetRiderDto : AddRiderDto { 
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
