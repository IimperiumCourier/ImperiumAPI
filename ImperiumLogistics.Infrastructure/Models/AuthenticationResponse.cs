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
        public string Name { get; set; } = string.Empty;
        public string Email { get; set;} = string.Empty;
        public string PhoneNumber { get; set;} = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid Id { get; set; }

        public static AuthenticationResponse GetResponse(string jwt, string reissueToken, string name,
                                                         string email, string phoneNo,string role,
                                                         Guid id)
        {
            return new AuthenticationResponse
            {
                Token = jwt,
                ReIssueToken = reissueToken,
                Email = email,
                PhoneNumber = phoneNo,
                Name = name,
                Role = role,
                Id = id
            };
        }
    }
}
