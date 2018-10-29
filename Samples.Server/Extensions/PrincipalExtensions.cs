using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Samples.Server
{
    public static class PrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var id = user?.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (!string.IsNullOrEmpty(id))
            {
                return new Guid(id);
            }

            return default;
        }
    }
}
