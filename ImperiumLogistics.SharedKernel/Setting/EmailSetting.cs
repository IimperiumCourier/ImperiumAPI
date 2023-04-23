using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Setting
{
    public class EmailSetting
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string PasswordUrl { get; set; } = string.Empty;
    }
}
