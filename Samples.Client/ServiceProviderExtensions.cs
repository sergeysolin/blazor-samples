using BlazorRedux;
using Microsoft.Extensions.DependencyInjection;
using Samples.Client.Infrastructure;
using Samples.Client.Infrastructure.ShoppingCart;
using Samples.Client.Infrastructure.Users;
using Samples.Client.Services;
using Samples.Shared.ShoppingCart;

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
            services.AddApiClient<UserCart>("/api/cart");

            return services;
        }

        public static IServiceCollection AddApiClient<T>(this IServiceCollection services, string baseUrl)
        {
            services.AddSingleton<IApiClient<T>, ApiClient<T>>((provider) => new ApiClient<T>(provider, baseUrl));

            return services;
        }
    }
}
