using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; } = string.Empty;
        public string ReIssueToken { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyEmail { get; set;} = string.Empty;
        public string CompanyPhone { get; set;} = string.Empty;

        public static AuthenticationResponse GetResponse(string jwt, string reissueToken, string name,
                                                         string email, string phoneNo)
        {
            return new AuthenticationResponse
            {
                Token = jwt,
                ReIssueToken = reissueToken,
                CompanyEmail = email,
                CompanyPhone = phoneNo,
                CompanyName = name
            };
        }
    }
}
