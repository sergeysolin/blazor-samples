using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.Users
{
    public class UserState : IIdentity
    {
        public bool IsAuthenticated { get; set; }

        public string AuthenticationType { get; set; }

        public string Name { get; set; }
    }
}
