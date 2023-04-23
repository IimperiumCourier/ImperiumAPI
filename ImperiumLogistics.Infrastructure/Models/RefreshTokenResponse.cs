using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Models
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public static RefreshTokenResponse GetRefreshToken(string accessToken, string refreshToken)
        {
            return new RefreshTokenResponse { AccessToken = accessToken,RefreshToken = refreshToken };
    }
}
