using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Samples.Client.Infrastructure;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.Users;
using Samples.Client.Services;

namespace Samples.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddReduxStore<ShoppingCartState, IAction>(new ShoppingCartState(), ShoppingCartReducer.Root);
            services.AddReduxStore<UserState, IAction>(new UserState(), UsersReducer.Root);

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IInventoryItemService, InventoryItemService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
