using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (User != null)
            {
                var user = await _userManager.FindByIdAsync(User.GetUserId());

                if (user != null)
                {
                    var profile = new UserProfile()
                    {
                        UserName = user.UserName,
                        Email = user.Email
                    };

                    return Ok(profile);
                }
            }

            return StatusCode(401);
        }
    }
}
