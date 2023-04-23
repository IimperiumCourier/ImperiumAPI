using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel
{
    public class Utility
    {
        public static int RefreshTokenValidityInDays = 1;
        public static DateTime GetNigerianTime() => DateTime.UtcNow.AddHours(1);

        public const string WelcomeTemplatePath = @"Templates\WelcomeTemplate.html";

        public static string WelcomeTemplate(string customerName, string companyName, string serviceUrl)
        {
            var path = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), WelcomeTemplatePath));
            var emailBody = path.ReadToEnd();
            emailBody = emailBody
                .Replace("[Customer Name]", customerName.ToSentenceCase())
                  .Replace("[Your Service URL]", serviceUrl)
                  .Replace("[Your Company Name]", companyName.ToSentenceCase());

            return emailBody;
        }
    }
}
