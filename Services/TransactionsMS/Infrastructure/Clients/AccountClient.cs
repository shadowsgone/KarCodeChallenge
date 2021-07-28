using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public class AccountClient : IAccountClient
    {
        private readonly ILogger<AccountClient> _logger;
        private readonly HttpClient _client;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public AccountClient(ILogger<AccountClient> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<bool> UpdateAccountBalanceAsync(Transaction transaction)
        {
            var content = new StringContent(JsonSerializer.Serialize(transaction, _jsonSerializerOptions), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{transaction.AccountId}/balance", content);

            return response.IsSuccessStatusCode;
        }
    }
}
