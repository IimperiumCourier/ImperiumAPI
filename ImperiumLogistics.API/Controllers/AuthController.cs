using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ServiceResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _authService.Authenticate(model.Email, model.Password);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(typeof(ServiceResponse<RefreshTokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var accesstoken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var res = await _authService.RefreshToken(accesstoken, request.RefreshToken);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
