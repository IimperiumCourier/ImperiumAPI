using ImperiumLogistics.Domain.AdminAggregate.Dto;
using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Implementation;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RiderController : ControllerBase
    {
        private readonly IRiderService riderService;
        public RiderController(IRiderService riderService)
        {
            this.riderService = riderService;
        }

        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<GetRiderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetAllRiders([FromBody] QueryRequest request)
        {
            List<string> acceptedRoles = new List<string> { UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<AdminDto>>.Error("Request is invalid."));
            }

            var response = await Task.FromResult(riderService.GetAllRiders(request));

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<GetRiderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public async Task<ActionResult> GetRider(Guid id)
        {

            ServiceResponse<GetRiderDto> res = await riderService.GetRider(id);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateAccount([FromBody] UpdateRiderDto model)
        {
            List<string> acceptedRoles = new List<string> { UserRoles.Rider };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var riderID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if (riderID == null)
            {
                return BadRequest(new ServiceResponse<GetRiderDto> { IsSuccessful = false, Message = "Request is invalid." });
            }

            model.Id = Guid.Parse(riderID.Value);

            var res = await riderService.UpdateRider(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateAccount([FromBody] AddRiderDto model)
        {
            List<string> acceptedRoles = new List<string> { UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await riderService.AddRider(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
