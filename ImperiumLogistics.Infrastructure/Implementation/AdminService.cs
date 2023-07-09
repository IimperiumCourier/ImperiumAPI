using Azure.Core;
using ImperiumLogistics.Domain.AdminAggregate;
using ImperiumLogistics.Domain.AdminAggregate.Dto;
using ImperiumLogistics.Domain.PackageAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Mapper;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.Infrastructure.AdminFilterHandler;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.ViewModel;
using ImperiumLogistics.Domain.AuthAggregate;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        private readonly IAuthRepo authRepository;

        public AdminService(IAdminRepository adminRepository, IAuthRepo authRepo)
        {
            this.adminRepository = adminRepository;
            this.authRepository = authRepo;
        }

        public async Task<ServiceResponse<string>> CreateAdmin(AdminCreationRequest request)
        {
            var admin = await adminRepository.Add(request.PhoneNumber, request.FullName, request.Email);

            _ = authRepository.CreateAsync(admin.CreateUser());
            var dbRes = await adminRepository.Save();
            if (dbRes < 0)
            {
                return ServiceResponse<string>.Error("Oops an error occured. Admin's record could not be created", "Oops an error occured. Admin's record could not be created");
            }

            return ServiceResponse<string>.Success("Oops an error occured. Admin's record could not be created", "Oops an error occured. Admin's record could not be created");
        }

        public async Task<ServiceResponse<string>> DeleteAdmin(string email, string requestedBy)
        {
            var admin = await adminRepository.GetByEmail(email);
            if (admin is null)
            {
                return ServiceResponse<string>.Error("Email is not connected to a profile", "Email is not connected to a profile");
            }

            var dbRes = await adminRepository.Save();
            if (dbRes < 0)
            {
                return ServiceResponse<string>.Error("Oops an error occured. Admin's record could not be created", "Oops an error occured. Admin's record could not be created");
            }

            return ServiceResponse<string>.Success("Oops an error occured. Admin's record could not be created", "Oops an error occured. Admin's record could not be created");

        }

        public ServiceResponse<PagedQueryResult<AdminDto>> GetAllAdmin(QueryRequest queryRequest)
        {
            PagedQueryResult<AdminDto> _result = new PagedQueryResult<AdminDto>();
            if (queryRequest != null)
            {

                IQueryable<Admin> response = adminRepository.GetAll();

                var filter = AdminHandlerFactory.GetAdminFilters();
                filter.Apply(ref response, queryRequest);

                int pageSize = queryRequest.PagedQuery != null ? queryRequest.PagedQuery.PageSize : Utility.DefaultPageSize;
                int pageNumber = queryRequest.PagedQuery != null ? queryRequest.PagedQuery.PageSize : Utility.DefaultPageSize;

                var result = response.ToPagedResult(pageNumber, pageSize);

                if (result.TotalItemCount <= 0)
                {
                    return ServiceResponse<PagedQueryResult<AdminDto>>.Success(new PagedQueryResult<AdminDto>
                    { Items = new List<AdminDto>() }, "There were no records found.");
                }

                var _data = result.Items.Select(e => AdminDto.GetAdmin(e)).ToList();

                _result.Items = _data;
                _result.TotalItemCount = result.TotalItemCount;
                _result.CurrentPageNumber = result.CurrentPageNumber;
                _result.CurrentPageSize = result.CurrentPageSize;
                _result.TotalPageCount = result.TotalPageCount;
                _result.HasPrevious = result.HasPrevious;
                _result.HasNext = result.HasNext;

                return ServiceResponse<PagedQueryResult<AdminDto>>.Success(_result);

            }
            else
            {
                return ServiceResponse<PagedQueryResult<AdminDto>>.Error("Request is invalid");
            }
        }
    }
}
