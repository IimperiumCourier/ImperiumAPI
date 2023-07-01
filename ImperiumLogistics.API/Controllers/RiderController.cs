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
using System.Net.Mime;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GodMode,Rider,Admin")]
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

        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateAccount([FromBody] AddRiderDto model)
        {
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
