using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ImperiumLogistics.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GodMode,Rider,Admin")]
    public class RiderController : ControllerBase
    {
    }
}
