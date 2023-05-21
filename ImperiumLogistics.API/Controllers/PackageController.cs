using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ImperiumLogistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IPackageDescriptionService _packageDescriptionService;

        public PackageController(IPackageService packageService, IPackageDescriptionService packageDescriptionService)
        {
            _packageService = packageService;
            _packageDescriptionService = packageDescriptionService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> Createpackage([FromBody] PackageDto package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _packageService.CreatePackage(package);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("id/{packageid}")]
        [ProducesResponseType(typeof(ServiceResponse<PackageQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetPackageByID(Guid packageid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PackageQueryResponse>.Error("Request is invalid."));
            }

            var res = await _packageService.GetPackage(packageid);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("trackingnumber/{trackingnum}")]
        [ProducesResponseType(typeof(ServiceResponse<PackageQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> GetPackageByTrackingNumber(string trackingnum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PackageQueryResponse>.Error("Request is invalid."));
            }

            var res = await _packageService.GetPackage(trackingnum);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<PackageQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetPackages([FromBody] PackageQueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<List<PackageQueryResponse>>.Error("Request is invalid."));
            }

            var res = _packageService.GetAllPackages(queryRequest);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet]
        [Route("description")]
        [ProducesResponseType(typeof(ServiceResponse<List<PackageDescQueryRes>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetPackageDescriptions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<List<PackageDescQueryRes>>.Error("Request is invalid."));
            }

            var res = _packageDescriptionService.GetPackageDescription();

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("id/{packageid}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePackageStatusUsingID(Guid packageid, [FromQuery] PackageStatus packageStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _packageService.UpdatePackageStatus(packageid, packageStatus);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("trackingnumber/{trackingnum}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePackageStatus(string trackingnum, [FromQuery] PackageStatus packageStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _packageService.UpdatePackageStatus(trackingnum, packageStatus);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("description")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePackageStatus([FromBody] CreateDescriptionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            var res = await _packageDescriptionService.AddPackageDescription(model.Description);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
