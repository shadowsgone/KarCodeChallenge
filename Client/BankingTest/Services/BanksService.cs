using BankingTest.Wrappers;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingTest.Services
{
    public class BanksService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly string _baseUrl;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public BanksService(IHttpClientWrapper httpClient, string url)
        {
            _httpClient = httpClient;
            _baseUrl = url;
        }

        public async Task<int> Create(string name)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Name = name,
            },
            _jsonSerializerOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed creating bank.");
            }

            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(data, _jsonSerializerOptions);
        }

        public async Task<int> AddAccount(int bankId, int accountId)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                BankId = bankId,
                AccountId = accountId,
            },
            _jsonSerializerOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/{bankId}/accounts", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed adding account to bank.");
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
