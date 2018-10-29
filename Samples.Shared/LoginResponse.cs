using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Samples.Shared
{
    public class LoginResponse
    {
        public string Scheme { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string RedirectUrl { get; set; }
    }
}
