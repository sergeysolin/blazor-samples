using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Samples.Client.Authorization;
using Samples.Client.Infrastructure;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.Users;
using Samples.Client.Services;
using System.Security.Principal;

namespace Samples.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddStores()
                .AddIdentity()
                .AddApiClients();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IUserService, UserService>();
            //services.AddSingleton<IInventoryItemService, InventoryItemService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
