using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.ViewModel
{
    public class PhoneNumberDTO
    {
        [Required(ErrorMessage = "Country code is required")]
        [MaxLength(4,ErrorMessage ="Country code can not be more than 4 digits")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Phonenumber is required")]
        public string Number { get; set; }
    }
}
