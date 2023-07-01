using ImperiumLogistics.Domain.AuthAggregate;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AdminAggregate
{
    public class Admin : Entity<Guid>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsSuperAdmin { get; private set; }

        public Admin(Guid id) : base(id)
        {

        }

        public Admin() : base(Guid.NewGuid())
        {

        }

        public static Admin GetAdmin(string name, string email, string phoneNo)
        {
            return new Admin
            {
                DateCreated = Utility.GetNigerianTime(),
                Email = Email.Add(email),
                FullName = name.ToSentenceCase(),
                PhoneNumber = phoneNo,
                IsActive = true
            };
        }

        public void Delete()
        {
            IsActive = false;
        }

        public User CreateUser()
        {
            return User.Create(new AddUserDto
            {
                InformationId = Id,
                Name = FullName,
                PhoneNumber = PhoneNumber,
                Password = Utility.DefaultAdminPassword,
                Role = UserRoles.Admin,
                UserName = Email.Address.RemoveSpace()
            });
        }
    }
}