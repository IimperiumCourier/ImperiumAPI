
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using ImperiumLogistics.SharedKernel.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GodMode,Company,Admin")]
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
    }
}
