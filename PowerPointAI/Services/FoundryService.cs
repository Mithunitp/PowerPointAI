using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PowerpointAi.Services
{
    public class FoundryRestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public FoundryRestService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<string> RunAgentAsync(string agentName, string input)
        {
            var endpoint = _config["Foundry:Endpoint"]!;
            var apiKey = _config["Foundry:ApiKey"]!;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = agentName,
                input = input
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Extract 'output' from JSON
            using var doc = JsonDocument.Parse(result);
            if (doc.RootElement.TryGetProperty("output", out var output))
            {
                return output.GetString() ?? string.Empty;
            }

            return string.Empty;
        }
    }
}
