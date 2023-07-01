using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AdminAggregate.Dto
{
    public class AdminDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public static AdminDto GetAdmin(Admin admin)
        {
            return new AdminDto { Id = admin.Id, FullName = admin.FullName,
                                  PhoneNumber = admin.PhoneNumber,Email = admin.Email.Address,
                                  IsActive = admin.IsActive };
        }       
    }
}
