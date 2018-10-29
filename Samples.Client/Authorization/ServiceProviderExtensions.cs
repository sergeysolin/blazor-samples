using BlazorRedux;
using Microsoft.Extensions.DependencyInjection;
using Samples.Client.Infrastructure.Users;
using System.Security.Principal;

namespace Samples.Client.Authorization
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddReduxStore<UserState, IAction>(new UserState(), UsersReducer.Root);
            services.AddSingleton<IIdentity, UserIdentity>();

            return services;
        }
    }
}
