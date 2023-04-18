using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.ViewModel
{
    public class CompanyPasswordCreationRequest
    {
        public Guid CompanyId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
