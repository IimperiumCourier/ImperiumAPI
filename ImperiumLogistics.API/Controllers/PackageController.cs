using ImperiumLogistics.Domain.PackageAggregate.DTO;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.APIWrapper;
using ImperiumLogistics.SharedKernel.Enums;
using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
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
        [Route("create")]
        [ProducesResponseType(typeof(ServiceResponse<PackageCreationRes>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> Createpackage([FromBody] PackageDto package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is invalid."));
            }
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if(roleClaim == null || roleClaim.Value != UserRoles.Company)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var companyID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (companyID == null)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }
            package.PackagePlacedBy = Guid.Parse(companyID.Value);
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

        [HttpPost]
        [Route("customerlist")]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<PackageQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetPackages([FromBody] PackageQueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is invalid."));
            }
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || roleClaim.Value != UserRoles.Company)
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var companyID = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (companyID == null)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is not authorized."));
            }
            var data = new PackageQueryRequestDTO
            {
                ComanyID = Guid.Parse(companyID.Value),
                DateFilter = queryRequest.DateFilter,
                PagedQuery = queryRequest.PagedQuery,
                TextFilter = queryRequest.TextFilter
            };

            var res = _packageService.GetAllPackages(data);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<PackageQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetAllPackage([FromBody] PackageQueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is invalid."));
            }

            var data = new PackageQueryRequestDTO
            {
                ComanyID = Guid.Empty,
                DateFilter = queryRequest.DateFilter,
                PagedQuery = queryRequest.PagedQuery,
                TextFilter = queryRequest.TextFilter
            };

            var res = _packageService.GetAllPackages(data);

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
        [Route("update")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePackageStatus([FromBody] UpdatePackageUsingTrackingNum model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<string>.Error("Request is invalid."));
            }

            List<string> acceptedRoles = new List<string> { UserRoles.Rider, UserRoles.Admin};
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var res = await _packageService.UpdatePackageStatus(model.TrackingNumber, model.Status);

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
        public async Task<ActionResult> AddPackageDescription([FromBody] CreateDescriptionModel model)
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

            var res = await _packageDescriptionService.AddPackageDescription(model.Description);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("assign")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignPackageToRider([FromBody] PackageAssignRequest model)
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

            var adminId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (adminId == null)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is not authorized."));
            }

            var res = await _packageService.AssignRiderToPackage(model, Guid.Parse(adminId.Value));

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("riderlist")]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<PackageQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetPackagesAssignedToRider([FromBody] RiderPackageQueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is invalid."));
            }

            List<string> acceptedRoles = new List<string> { UserRoles.Rider, UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }

            var res = _packageService.GetAllPackageAssignedToRider(queryRequest);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("adminlist")]
        [ProducesResponseType(typeof(ServiceResponse<PagedQueryResult<PackageQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetPackagesForAdminDashboard([FromBody] PackageQueryRequest queryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ServiceResponse<PagedQueryResult<PackageQueryResponse>>.Error("Request is invalid."));
            }
            List<string> acceptedRoles = new List<string> { UserRoles.Admin };
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !acceptedRoles.Contains(roleClaim.Value))
            {
                return BadRequest(ServiceResponse<PackageCreationRes>.Error("Request is not authorized."));
            }


            var request = new PackageQueryRequestDTO
            {
                DateFilter = queryRequest.DateFilter,
                PagedQuery = queryRequest.PagedQuery,
                TextFilter = queryRequest.TextFilter
            };

            var res = _packageService.GetAllPackagesInSystem(request);

            if (!res.IsSuccessful)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
