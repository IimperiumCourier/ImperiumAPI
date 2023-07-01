using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<RefreshTokenResponse>> RefreshToken(string accessToken, string refreshToken);
        Task<ServiceResponse<AuthenticationResponse>> Authenticate(string username, string password);
    }
}
