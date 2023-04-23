using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface ITokenGenerator
    {
        string GenerateToken(string username, Guid? userId, string role);

        List<string>? ValidateToken(string token);
        string GenerateRefreshToken();
    }
}
