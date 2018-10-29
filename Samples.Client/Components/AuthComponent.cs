using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Samples.Client.Infrastructure.Users;
using Samples.Client.Infrastructure.Users.Actions;
using Samples.Client.Services;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Components
{
    public class AuthComponent : ReduxComponent<UserState, IAction>
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        protected async Task SignIn(string email, string password)
        {
            var response = await AuthService.LoginAsync(new LoginRequest()
            {
                Email = email,
                Password = password
            });

            Dispatch(new SignInAction() { Login = response });

            StateHasChanged();

            //if(!string.IsNullOrEmpty(response.RedirectUrl))
            //{
            //    UriHelper.NavigateTo(response.RedirectUrl);
            //}
        }

        protected async Task SignOut()
        {
            await AuthService.SignOutAsync();

            Dispatch(new SignOutAction());
        }

        protected async Task Register(string email, string password)
        {
            var response = await AuthService.RegisterAsync(new LoginRequest()
            {
                Email = email,
                Password = password
            });

            Dispatch(new SignInAction() { Login = response });

            StateHasChanged();

            //if (!string.IsNullOrEmpty(response.RedirectUrl))
            //{
            //    UriHelper.NavigateTo(response.RedirectUrl);
            //}
        }
    }
}
