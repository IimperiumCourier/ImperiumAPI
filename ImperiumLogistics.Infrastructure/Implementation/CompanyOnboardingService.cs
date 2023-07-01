using Azure.Core;
using ImperiumLogistics.Domain.AuthAggregate;
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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IEmailService _emailService;
        private readonly EmailSetting _emailSetting;
        private readonly IAuthRepo _authRepo;
        private readonly ITokenGenerator _tokenGenerator;
        public CompanyService(ICompanyRepository companyRepository,
                              IEmailService emailService,
                              IOptions<EmailSetting> emailSettingOption,
                              IAuthRepo authRepo,
                              ITokenGenerator tokenGenerator)
        {
            _companyRepo = companyRepository;
            _emailService = emailService;
            _emailSetting = emailSettingOption.Value;
            _authRepo = authRepo;
            _tokenGenerator = tokenGenerator;
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
            });

            return ServiceResponse<string>.Success("An email verification link has been sent to you, kindly check your inbox or spam to verify your email.");
        }

        public async Task<ServiceResponse<AuthenticationResponse>> CreatePassword(CompanyPasswordCreationRequest request)
        {
            var company = await _companyRepo.GetById(request.CompanyId);
            var user = await _authRepo.GetByInfoIdAsync(request.CompanyId);

            if (company is null || user is null)
            {
                return ServiceResponse<AuthenticationResponse>.Error("Hi, company identifier provided is invalid.");
            }
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return ServiceResponse<AuthenticationResponse>.Error("Password confirmation failed, ensure your password is correct.");
            }

            user.AddRefreshToken(_tokenGenerator.GenerateRefreshToken(),
                                Utility.GetNigerianTime().
                                AddDays(Utility.RefreshTokenValidityInDays));
            user.UpdatePassword(request.Password);
            _ = await _authRepo.UpdateAsync(user);

            company.EmailVerified();
            _companyRepo.Update(company);

            var response = await _companyRepo.Save();

            if (response < 1)
            {
                return ServiceResponse<AuthenticationResponse>.Error("An error occurred while saving record.");
            }

            var tokenData = _tokenGenerator.GenerateToken(company.EmailAddress.Address, company.Id, user.Role);

            var authRes = AuthenticationResponse
                .GetResponse(tokenData, user.RefreshToken, company.Name, company.EmailAddress.Address, company.PhoneNumber, user.Role);

            return ServiceResponse<AuthenticationResponse>.Success(authRes,"Password set successfully.");
        }

    }
}
