using BankingTest.Wrappers;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingTest.Services
{
    public class UsersService : IDisposable
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _baseUrl;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public UsersService(IHttpClientWrapper httpClient, string url)
        {
            _httpClient = httpClient;
            _baseUrl = url;
        }

        public async Task<int> Create(string firstName, string lastName, bool isActive)
        {
            var content = new StringContent(JsonSerializer.Serialize(new 
            { 
                FirstName = firstName,
                LastName = lastName,
                IsActive = isActive,
            }, 
            _jsonSerializerOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed creating user.");
            }

            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(data, _jsonSerializerOptions);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
