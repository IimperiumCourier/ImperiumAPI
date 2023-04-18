using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
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
        public CompanyOnboardingService(ICompanyRepository companyRepository, IEmailService emailService)
        {
            _companyRepo = companyRepository;
            _emailService = emailService;
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

            _ = await _companyRepo.Add(request.PhoneNumber.ConvertToElevenDigits(), request.Address, request.City,
                                           request.State, request.FirstName, request.LastName,
                                           request.Email);

            var dbResponse = await _companyRepo.Save();

            if(dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            // Send Email to company
            //await _emailService.SendMail(request.Email, string.Empty);

            return ServiceResponse<string>.Success("An email verification link has been sent to you, kindly check your inbox or spam to verify your email.");
        }

        public async Task<ServiceResponse<string>> CreatePassword(CompanyPasswordCreationRequest request)
        {
            var company = await _companyRepo.GetById(request.CompanyId);
            if(company is null)
            {
                return ServiceResponse<string>.Error("Hi, company identifier provided is invalid.");
            }
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return ServiceResponse<string>.Error("Password confirmation failed, ensure your password is correct.");
            }

            company.AddPassword(request.Password);
            _companyRepo.Update(company);

            var response = await _companyRepo.Save();

            if (response < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            return ServiceResponse<string>.Success("Password set successfully.");
        }
    }
}
