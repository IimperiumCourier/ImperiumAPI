using ImperiumLogistics.Domain.AdminAggregate.Dto;
using ImperiumLogistics.Domain.RiderAggregate.Dto;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Implementation;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GodMode, Rider, Admin")]
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
        [ProducesResponseType(typeof(ServiceResponse<GetRiderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetRider()
        {
            var riderID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if(riderID == null)
            {
                return BadRequest(new ServiceResponse<GetRiderDto> { IsSuccessful = false, Message = "Request is invalid." });
            }

            ServiceResponse<GetRiderDto> res = await riderService.GetRider(Guid.Parse(riderID.Value));

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await riderService.UpdateRider(model);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
