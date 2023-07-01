using ImperiumLogistics.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class ChangeAdminPassword
    {
        [Required(ErrorMessage = "Email is a required property")]
        public string Email { get; set; }

        [Password(ErrorMessage = "Password is invalid")]
        public string Password { get; set; }
    }
}
