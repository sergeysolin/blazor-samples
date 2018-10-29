using Microsoft.AspNetCore.Blazor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Samples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.Client.Services
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string url);
        Task<T> SaveAsync<T>(string url, object data);
    }

    public interface IApiClient<T>
    {
        Task<Response> GetAsync(CancellationToken cancellationToken);
        Task<Response> GetAsync();

        Task<Response> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken);
        Task<Response> GetByIdAsync<TKey>(TKey id);

        Task<Response> CreateAsync(T data, CancellationToken cancellationToken);
        Task<Response> CreateAsync(T data);

        Task<Response> UpdateAsync<TKey>(TKey id, T data, CancellationToken cancellationToken);
        Task<Response> UpdateAsync<TKey>(TKey id, T data);

        Task<Response> DeleteAsync<TKey>(TKey id, CancellationToken cancellationToken);
        Task<Response> DeleteAsync<TKey>(TKey id);
    }

    public class ApiClient<T> : IApiClient<T>
    {
        private readonly string _baseUrl;
        protected readonly HttpClient _httpClient;

        public ApiClient(IServiceProvider serviceProvider, string baseUrl)
        {
            _httpClient = serviceProvider.GetRequiredService<HttpClient>();
            _baseUrl = baseUrl;
        }

        protected async Task<Response> SendAsync(string url, HttpMethod method, object content, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            try
            {
                if(method == HttpMethod.Get)
                {
                    response = await _httpClient.GetAsync(url, httpCompletionOption, cancellationToken);
                }
                else
                {
                    var requestJson = content == null ? string.Empty : Json.Serialize(content);

                    response = await _httpClient.SendAsync(new HttpRequestMessage(method, url)
                    {
                        Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
                    }, httpCompletionOption, cancellationToken);
                }

                response.EnsureSuccessStatusCode();

                var data = default(T);

                if(httpCompletionOption == HttpCompletionOption.ResponseContentRead)
                {
                    data = Json.Deserialize<T>(await response.Content.ReadAsStringAsync());                    
                }                

                return new Response(data)
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase
                };
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");

                if(response != null)
                {
                    return new Response(response.StatusCode, response.ReasonPhrase);
                }                
            }

            return new Response(System.Net.HttpStatusCode.InternalServerError, "Error occurred making request.");
        }

        #region Get

        public Task<Response> GetAsync(CancellationToken cancellationToken) =>
            SendAsync(_baseUrl, HttpMethod.Get, null, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public Task<Response> GetAsync() =>
            GetAsync(CancellationToken.None);

        #endregion

        #region Get By Id

        public Task<Response> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken) =>
            SendAsync($"{_baseUrl}/{id}", HttpMethod.Get, null, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public Task<Response> GetByIdAsync<TKey>(TKey id) =>
            GetByIdAsync(id, CancellationToken.None);

        #endregion

        #region Create

        public Task<Response> CreateAsync(T data, CancellationToken cancellationToken) =>
            SendAsync(_baseUrl, HttpMethod.Post, data, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public Task<Response> CreateAsync(T data) =>
            CreateAsync(data, CancellationToken.None);

        #endregion

        #region Update

        public Task<Response> UpdateAsync<TKey>(TKey id, T data, CancellationToken cancellationToken) =>
            SendAsync($"{_baseUrl}/{id}", HttpMethod.Put, data, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public Task<Response> UpdateAsync<TKey>(TKey id, T data) =>
            UpdateAsync(id, data, CancellationToken.None);

        #endregion

        #region Delete

        public Task<Response> DeleteAsync<TKey>(TKey id, CancellationToken cancellationToken) =>
            SendAsync($"{_baseUrl}/{id}", HttpMethod.Delete, null, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        public Task<Response> DeleteAsync<TKey>(TKey id) =>
            DeleteAsync(id, CancellationToken.None);

        #endregion
    }

    public class ApiClient : IApiClient
    {
        protected readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetJsonAsync<T>(url);
            return response;
        }

        public async Task<T> SaveAsync<T>(string url, object data)
        {
            var response = await _httpClient.PostJsonAsync<T>(url, data);
            return response;
        }
    }
}
