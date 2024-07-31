using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GearGauge.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("check-authentication")]
        public IActionResult CheckAuthentication()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { isAuthenticated = true });
            }
            else
            {
                return Ok(new { isAuthenticated = false });
            }
        }
    }
}
