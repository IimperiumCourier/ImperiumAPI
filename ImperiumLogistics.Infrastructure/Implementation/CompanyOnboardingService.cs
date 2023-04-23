using Azure.Core;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Setting;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class CompanyOnboardingService : ICompanyOnboardingService
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IEmailService _emailService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly EmailSetting _emailSetting;
        public CompanyOnboardingService(ICompanyRepository companyRepository, IEmailService emailService,
                                        ITokenGenerator tokenGenerator, IOptions<EmailSetting> emailSettingOption)
        {
            _companyRepo = companyRepository;
            _emailService = emailService;
            _tokenGenerator = tokenGenerator;
            _emailSetting = emailSettingOption.Value;
        }
        public async Task<ServiceResponse<string>> CreateAccount(CompanyAccountCreationRequest request)
        {
            string phoneNumber = request.PhoneNumber.ConvertToElevenDigits();

            if(await _companyRepo.HasCompanyAccount(phoneNumber))
            {
                return ServiceResponse<string>.Error($"{phoneNumber} is tied to an account.");
            }

            if(await _companyRepo.HasCompanyAccount(request.Email))
            {
                return ServiceResponse<string>.Error($"{request.Email} is tied to an account.");
            }

            var company = await _companyRepo.Add(request.PhoneNumber.ConvertToElevenDigits(), request.Address, request.City,
                                           request.State, request.ContactFullName, request.CompanyName,
                                           request.Email);

            var dbResponse = await _companyRepo.Save();

            if(dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            string passwordUrl = $"{_emailSetting.PasswordUrl}{company.Id}";
            await _emailService.SendMail(new EmailRequestDto
            {
                Body = Utility.WelcomeTemplate(request.ContactFullName, request.CompanyName, passwordUrl),
                IsHtml= true,
                Recepient = request.Email,
                Subject = "Welcome Onboard."
            }); ;

            return ServiceResponse<string>.Success("An email verification link has been sent to you, kindly check your inbox or spam to verify your email.");
        }

        public async Task<ServiceResponse<AuthenticationResponse>> CreatePassword(CompanyPasswordCreationRequest request)
        {
            var company = await _companyRepo.GetById(request.CompanyId);
            if(company is null)
            {
                return ServiceResponse<AuthenticationResponse>.Error("Hi, company identifier provided is invalid.");
            }
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return ServiceResponse<AuthenticationResponse>.Error("Password confirmation failed, ensure your password is correct.");
            }

            company.AddRefreshToken(_tokenGenerator.GenerateRefreshToken(),
                                Utility.GetNigerianTime().
                                AddDays(Utility.RefreshTokenValidityInDays));

            company.AddPassword(request.Password);
            _companyRepo.Update(company);

            var response = await _companyRepo.Save();

            if (response < 1)
            {
                return ServiceResponse<AuthenticationResponse>.Error("An error occurred while saving record.");
            }

            var tokenData = _tokenGenerator.GenerateToken(company.EmailAddress.Address, company.Id, UserRoles.Company);

            var authRes = AuthenticationResponse
                .GetResponse(tokenData, company.RefreshToken, company.Name, company.EmailAddress.Address, company.PhoneNumber);

            return ServiceResponse<AuthenticationResponse>.Success(authRes,"Password set successfully.");
        }

        public async Task<ServiceResponse<AuthenticationResponse>> Authenticate(string username, string password)
        {
            string _email = username.ToSentenceCase();
            var company = await _companyRepo.GetByEmail(_email);
            if (company is null)
            {
                return ServiceResponse<AuthenticationResponse>.Error("Username or password is invalid.");
            }

            if (company.HasNotSetPassword())
            {
                return ServiceResponse<AuthenticationResponse>.Error("Password has not been set for your account. Set your password to continue.");
            }

            if(!company.Credential.ValidatePassword(password))
            {
                return ServiceResponse<AuthenticationResponse>.Error("Username or password is invalid.");
            }

            company.AddRefreshToken(_tokenGenerator.GenerateRefreshToken(),
                                Utility.GetNigerianTime().
                                AddDays(Utility.RefreshTokenValidityInDays));
            _companyRepo.Update(company);

            var response = await _companyRepo.Save();

            if (response < 1)
            {
                return ServiceResponse<AuthenticationResponse>.Error("An error occurred while completing request.");
            }


            var tokenData = _tokenGenerator.GenerateToken(company.EmailAddress.Address, company.Id, UserRoles.Company);

            return ServiceResponse<AuthenticationResponse>.Success(AuthenticationResponse
                .GetResponse(tokenData, company.RefreshToken, company.Name, company.EmailAddress.Address, company.PhoneNumber));
        }

        public async Task<ServiceResponse<RefreshTokenResponse>> RefreshToken(string token)
        {
            var claims = _tokenGenerator.ValidateToken(token);
            if (claims is null || claims.Count <= 0)
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Something went wrong, token is not valid.");
            }

            string _email = claims[0];
            var company = await _companyRepo.GetByEmail(_email);
            if (company is null)
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Something went wrong, token could not be validated.");
            }

            

            if(company.HasNoRefreshToken() || company.HasExpiredRefreshToken())
            {
                return ServiceResponse<RefreshTokenResponse>.Error("Invalid access token or refresh token");
            }

            company.UpdateRefreshToken(_tokenGenerator.GenerateRefreshToken());
            _companyRepo.Update(company);

            var tokenData = _tokenGenerator.GenerateToken(company.EmailAddress.Address, company.Id, UserRoles.Company);

            return ServiceResponse<RefreshTokenResponse>.Success(RefreshTokenResponse.GetRefreshToken(tokenData, company.RefreshToken));
        }
    }
}
