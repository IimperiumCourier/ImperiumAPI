
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ImperiumLogistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyOnboardingService _onboardingService;
        public CompanyController(ICompanyOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
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
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
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
