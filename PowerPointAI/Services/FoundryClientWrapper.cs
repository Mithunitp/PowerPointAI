using Microsoft.Azure.AI.Foundry;
using Microsoft.Azure.AI.Foundry.Models;
using Microsoft.Extensions.Options;
using PowerpointAi.Models;

namespace PowerpointAi.Services
{
    public class FoundryClientWrapper
    {
        private readonly FoundryClient _client;
        private readonly string _projectId;

        public FoundryClientWrapper(IOptions<FoundryOptions> options)
        {
            var config = options.Value;
            _projectId = config.ProjectId;
            _client = new FoundryClient(new Uri(config.Endpoint), new Azure.AzureKeyCredential(config.ApiKey));
        }

        public async Task<string> RunAgentAsync(string agentName, string input)
        {
            var request = new RunAgentRequest
            {
                ProjectId = _projectId,
                AgentName = agentName,
                Input = input
            };

            var response = await _client.Agents.RunAsync(request);
            return response.Output;
        }

        // Optional: retrieval for RAG
        public async Task<List<string>> RetrieveDocumentsAsync(string query, int topK = 3)
        {
            var results = await _client.Retrieval.QueryAsync(_projectId, query, topK);
            return results.Select(r => r.Content).ToList();
        }
    }
}
