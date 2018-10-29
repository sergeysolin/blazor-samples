using BlazorRedux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.Users
{
    public class UserIdentity : IIdentity
    {
        private readonly Store<UserState, IAction> _store;

        public UserIdentity(Store<UserState, IAction> store) => _store = store;

        public string AuthenticationType => _store?.State?.AuthenticationType;

        public bool IsAuthenticated => _store?.State?.IsAuthenticated ?? false;

        public string Name => _store?.State?.Name;
    }
}
