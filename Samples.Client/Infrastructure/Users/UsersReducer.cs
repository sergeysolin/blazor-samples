using BlazorRedux;
using Samples.Client.Infrastructure.Users.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Infrastructure.Users
{
    public static class UsersReducer
    {
        public static UserState Root(UserState state, IAction action)
        {
            if(state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            switch (action)
            {
                case SignInAction signIn:
                    return new UserState()
                    {
                        AuthenticationType = signIn?.Login?.Scheme,
                        IsAuthenticated = true,
                        Name = signIn?.Login?.UserName
                    };
                case SignOutAction _:
                    return new UserState();
                default:
                    break;
            }

            return state;
        }
    }
}
