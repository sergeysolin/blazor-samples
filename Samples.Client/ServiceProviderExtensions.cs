using BlazorRedux;
using Microsoft.Extensions.DependencyInjection;
using Samples.Client.Infrastructure;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.Users;
using Samples.Client.Services;
using Samples.Client.Services.Clients;
using Samples.Shared.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Samples.Client
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddStores(this IServiceCollection services)
        {
            services.AddReduxStore<ShoppingCartState, IAction>(new ShoppingCartState(), ShoppingCartReducer.Root);
            services.AddReduxStore<UserState, IAction>(new UserState(), UsersReducer.Root);

            return services;
        }

        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton<IApiClient<UserCart>, ApiClient<UserCart>>((provider) => new ApiClient<UserCart>(provider, "/api/cart"));



            return services;
        }
    }
}
