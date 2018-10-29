using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Samples.Server.Services;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(IJwtTokenService jwtTokenService, UserManager<IdentityUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Id = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(500);
            }
            
            return Ok(await BuildResponseAsync(model.Email, user.Id));
        }

        async Task<LoginResponse> BuildResponseAsync(string email, string id)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return new LoginResponse()
            {
                RedirectUrl = null,
                Roles = await _userManager.GetRolesAsync(user),
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                UserName = user.UserName ?? user.Email,
                Token = _jwtTokenService.BuildToken(user.Email, id)
            };
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!correctUser)
            {
                return BadRequest();
            }

            return Ok(await BuildResponseAsync(model.Email, user.Id));
        }
    }
}
