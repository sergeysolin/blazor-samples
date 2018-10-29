using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace Samples.Client.Services
{
    public interface IUserService
    {
        Task<UserProfile> GetProfileAsync();
    }

    public class UserService : ApiClient, IUserService
    {
        public UserService(HttpClient httpClient) : base(httpClient) { }

        public async Task<UserProfile> GetProfileAsync()
        {
            return await _httpClient.GetJsonAsync<UserProfile>("/api/user");
        }
    }
}
