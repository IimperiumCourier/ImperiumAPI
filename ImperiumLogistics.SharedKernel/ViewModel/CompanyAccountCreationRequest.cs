using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.ViewModel
{
    public class CompanyAccountCreationRequest
    {
        public string ContactFullName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public PhoneNumberDTO PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class CompanyAccountUpdateRequest
    {
        public string ContactFullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
