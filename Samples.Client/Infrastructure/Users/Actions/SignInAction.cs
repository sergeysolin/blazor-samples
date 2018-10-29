using BlazorRedux;
using Samples.Shared;

namespace Samples.Client.Infrastructure.Users.Actions
{
    public class SignInAction : IAction
    {
        public LoginResponse Login { get; set; }
    }
}
