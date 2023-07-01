using Azure.Core;
using ImperiumLogistics.Domain.RiderAggregate;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    internal class RiderService : IRiderService
    {
        private readonly IRiderRepository riderRepository;
        private readonly IEmailService emailService;
        public RiderService(IRiderRepository riderRepository, IEmailService emailService)
        {
            this.riderRepository = riderRepository;
            this.emailService = emailService;
        }

        public async Task<ServiceResponse<string>> AddRider(AddRiderDto rider)
        {
            if(await riderRepository.IsConnectedToRecord(rider.Email))
            {
                return ServiceResponse<string>.Error($"{rider.Email} is tied to an account.");
            }

            if (await riderRepository.IsConnectedToRecord(rider.PhoneNumber))
            {
                return ServiceResponse<string>.Error($"{rider.PhoneNumber} is tied to an account.");
            }

            var _rider = await riderRepository.AddRider(rider);

            var dbResponse = await riderRepository.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }

            await emailService.SendMail(new EmailRequestDto
            {
                Body = Utility.RiderWelcomeTemplate(rider.Email, Utility.DefaultRiderPassword),
                IsHtml = true,
                Recepient = rider.Email,
                Subject = "Welcome Onboard."
            });

            return ServiceResponse<string>.Success("An email has been sent to rider, kindly advice rider to check his/her inbox or spam for login credentials.");
        }

        public Task<PagedQueryResult<GetRiderDto>> GetAllRiders(QueryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetRiderDto>> GetRider(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
