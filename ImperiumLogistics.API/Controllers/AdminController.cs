using ImperiumLogistics.Domain.AdminAggregate.Dto;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Implementation;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ImperiumLogistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IPackageService packageService;

        public AdminController(IAdminService adminService, IPackageService packageService)
        {
            this.adminService = adminService;
            this.packageService = packageService;
        }

        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> CreateAccount([FromBody] AdminCreationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await adminService.CreateAdmin(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<AdminDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetAllAdmins([FromBody] QueryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<AdminDto>>.Error("Request is invalid."));
            }

            var response = await Task.FromResult(adminService.GetAllAdmin(request));

            if (!response.IsSuccessful){
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("analytics")]
        [ProducesResponseType(typeof(ServiceResponse<PackageAnalytics>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetAnalytics()
        {
            List<string> acceptedRoles = new List<string> { UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageAnalytics>.Error("Request is not authorized."));
            }

            var response = await packageService.GetPackageAnalytics();
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
