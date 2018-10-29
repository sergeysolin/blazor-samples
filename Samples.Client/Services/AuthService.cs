using Microsoft.AspNetCore.Blazor;
using Samples.Shared;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Samples.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> RegisterAsync(LoginRequest request);
        Task SignOutAsync();
    }

    public class AuthService : ApiClient, IAuthService
    {
        public AuthService(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response =  await _httpClient.PostJsonAsync<LoginResponse>("/api/token/login", request);

            SetAuthToken(response);

            return response;
        }

        public async Task<LoginResponse> RegisterAsync(LoginRequest request)
        {
            var response = await _httpClient.PostJsonAsync<LoginResponse>("/api/token/registration", request);

            SetAuthToken(response);

            return response;
        }

        public Task SignOutAsync()
        {
            SetAuthToken(null);

            return Task.CompletedTask;
        }

        private void SetAuthToken(LoginResponse response)
        {
            AuthenticationHeaderValue header = null;

            if (!string.IsNullOrEmpty(response?.Token))
            {
                header = new AuthenticationHeaderValue(response.Scheme ?? "Bearer", response.Token);
            }

            _httpClient.DefaultRequestHeaders.Authorization = header;
        }
    }
}
