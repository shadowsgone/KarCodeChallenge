using BankingTest.Models;
using BankingTest.Wrappers;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingTest.Services
{
    public class AccountsService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _baseUrl;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public AccountsService(IHttpClientWrapper httpClient, string url)
        {
            _httpClient = httpClient;
            _baseUrl = url;
        }

        public async Task<int> Create(string name, AccountTypes type, int ownerId, int bankId, decimal balance)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Name = name,
                Type = type,
                BankId = bankId,
                OwnerId = ownerId,
                Balance = balance,
            },
            _jsonSerializerOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed creating account.");
            }

            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(data, _jsonSerializerOptions);
        }

        public async Task<decimal> GetBalance(int accountId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{accountId}/balance");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed creating bank.");
            }

            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<decimal>(data, _jsonSerializerOptions);

        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
