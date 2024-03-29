﻿
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using ImperiumLogistics.SharedKernel.Enums;
using Microsoft.AspNetCore.Authorization;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _onboardingService;
        public CompanyController(ICompanyService onboardingService)
        {
            _onboardingService = onboardingService;
        }

        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> CreateAccount([FromBody] CompanyAccountCreationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _onboardingService.CreateAccount(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("credential")]
        [ProducesResponseType(typeof(ServiceResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> AddPassword([FromBody] CompanyPasswordCreationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _onboardingService.CreatePassword(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("analytics")]
        [ProducesResponseType(typeof(ServiceResponse<BusinessAnalytics>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> GetAnalytics([FromQuery] Guid companyId)
        {
            var res = await _onboardingService.GetAnalytics(companyId);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(ServiceResponse<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult GetListOfCompanies([FromBody] QueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            List<string> acceptedRoles = new List<string> { UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var response = _onboardingService.GetAllCompanies(queryRequest);
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(ServiceResponse<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateAccount([FromBody] CompanyAccountUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<Company>.Error("Request is invalid."));
            }

            List<string> acceptedRoles = new List<string> { UserRoles.Company };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var companyID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (companyID == null)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            model.Id = Guid.Parse(companyID.Value);

            var res = await _onboardingService.UpdateAccount(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("detail")]
        [ProducesResponseType(typeof(ServiceResponse<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetDetail()
        {
            List<string> acceptedRoles = new List<string> { UserRoles.Company };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var companyID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (companyID == null)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }
            Guid companyId = Guid.Parse(companyID.Value);

            var res = await _onboardingService.GetCompanyInformation(companyId);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
