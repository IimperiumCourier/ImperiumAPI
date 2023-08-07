using ImperiumLogistics.Domain.AuthAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel;
using System.IdentityModel.Tokens.Jwt;
using ImperiumLogistics.Domain.CompanyAggregate;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo authRepo;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthService(IAuthRepo authRepo,ITokenGenerator tokenGenerator)
        {
            this.authRepo = authRepo;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ServiceResponse<AuthenticationResponse>> Authenticate(string username, string password)
        {
            string _email = username.ToSentenceCase();
            var user = await authRepo.GetAsync(_email);
            if (user is null)
            {
                return ServiceResponse<AuthenticationResponse>.Error("Username or password is invalid.");
            }

            if (!user.Credential.ValidatePassword(password))
            {
                return ServiceResponse<AuthenticationResponse>.Error("Username or password is invalid.");
            }

            user.AddRefreshToken(_tokenGenerator.GenerateRefreshToken(),
                                Utility.GetNigerianTime().
                                AddDays(Utility.RefreshTokenValidityInDays));
            await authRepo.UpdateAsync(user);

            var tokenData = _tokenGenerator.GenerateToken(user.UserName, user.InformationId, user.Role);

            return ServiceResponse<AuthenticationResponse>.Success(AuthenticationResponse
            .GetResponse(tokenData, user.RefreshToken, user.Name,
                         user.UserName, user.PhoneNumber, user.Role,
                         user.InformationId));

        }

        public async Task<ServiceResponse<RefreshTokenResponse>> RefreshToken(string accessToken, string refreshToken)
        {
            var claims = _tokenGenerator.ValidateToken(accessToken);
            if (claims is null || claims.Count <= 0)
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Something went wrong, token is not valid.");
            }

            string _email = claims[JwtRegisteredClaimNames.Name];
            var user = await authRepo.GetAsync(_email);
            if (user is null)
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Something went wrong, token could not be validated.");
            }



            if (user.HasNoRefreshToken() || user.HasExpiredRefreshToken() || user.RefreshToken != refreshToken)
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Invalid access token or refresh token");
            }

            user.UpdateRefreshToken(_tokenGenerator.GenerateRefreshToken());
            _ = await authRepo.UpdateAsync(user);

            var tokenData = _tokenGenerator.GenerateToken(user.UserName, user.InformationId, user.Role);

            return ServiceResponse<RefreshTokenResponse>.Success(RefreshTokenResponse.GetRefreshToken(tokenData, user.RefreshToken));
        }


    }
}
