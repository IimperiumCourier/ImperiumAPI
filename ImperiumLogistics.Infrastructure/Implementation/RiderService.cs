using Azure.Core;
using ImperiumLogistics.Domain.AdminAggregate.Dto;
using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.RiderAggregate;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.AdminFilterHandler;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.Infrastructure.Repository;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperiumLogistics.Infrastructure.RiderHandlers;
using ImperiumLogistics.Domain.AuthAggregate;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepository riderRepository;
        private readonly IEmailService emailService;
        private readonly IAuthRepo authRepo;
        public RiderService(IRiderRepository riderRepository, IEmailService emailService, IAuthRepo _authRepo)
        {
            this.riderRepository = riderRepository;
            this.emailService = emailService;
            this.authRepo = _authRepo;
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

            _ = await authRepo.CreateAsync(_rider.CreateUser());

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

        public ServiceResponse<PagedQueryResult<GetRiderDto>> GetAllRiders(QueryRequest request)
        {
            PagedQueryResult<GetRiderDto> _result = new PagedQueryResult<GetRiderDto>();
            if (request != null)
            {

                IQueryable<Rider> response = riderRepository.GetAll();

                var filter = RiderHandlerFactory.GetAdminFilters();
                filter.Apply(ref response, request);

                int pageSize = request.PagedQuery.PageSize();
                int pageNumber = request.PagedQuery.PageNumber();

                var result = response.ToPagedResult(pageNumber, pageSize);

                if (result.TotalItemCount <= 0)
                {
                    return ServiceResponse<PagedQueryResult<GetRiderDto>>.Success(new PagedQueryResult<GetRiderDto>
                    { Items = new List<GetRiderDto>() }, "There were no records found.");
                }

                var _data = result.Items.Select(e => GetRiderDto.Create(e)).ToList();

                _result.Items = _data;
                _result.TotalItemCount = result.TotalItemCount;
                _result.CurrentPageNumber = result.CurrentPageNumber;
                _result.CurrentPageSize = result.CurrentPageSize;
                _result.TotalPageCount = result.TotalPageCount;
                _result.HasPrevious = result.HasPrevious;
                _result.HasNext = result.HasNext;

                return ServiceResponse<PagedQueryResult<GetRiderDto>>.Success(_result);

            }
            else
            {
                return ServiceResponse<PagedQueryResult<GetRiderDto>>.Error("Request is invalid");
            }
        }

        public async Task<ServiceResponse<GetRiderDto>> GetRider(Guid id)
        {
            var rider = await riderRepository.GetRider(id);

            if (rider == null)
            {
                return ServiceResponse<GetRiderDto>.Error("Specified Identification number is no connected to a rider");
            }

            var response = GetRiderDto.Create(rider);
            return ServiceResponse<GetRiderDto>.Success(response);
        }

        public async Task<ServiceResponse<string>> UpdateRider(UpdateRiderDto rider)
        {
            var _rider = await riderRepository.GetRider(rider.Id);

            if (_rider == null)
            {
                return ServiceResponse<string>.Error($"Request is invalid.");
            }

            _rider.Update(rider);

            riderRepository.UpdateRider(_rider);
            var dbResponse = await riderRepository.Save();

            if (dbResponse < 1)
            {
                return ServiceResponse<string>.Error("An error occurred while saving record.");
            }
            return ServiceResponse<string>.Success("Hi, your details were updated successfully.");
        }

    }
}
