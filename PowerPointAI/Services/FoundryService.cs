using Azure;
using Azure.AI.Projects;

namespace PowerpointAi.Services
{
    public class FoundryService
    {
        private readonly AIProjectClient _client;
        private readonly string _projectId;

        // Dictionary to store agent "definitions" (name → systemPrompt)
        private readonly Dictionary<string, string> _agents = new();

        public FoundryService(AIProjectClient client, IConfiguration config)
        {
            _client = client;
            _projectId = config["AzureAI:ProjectId"]!;
        }

        public async Task<string> RunAgentAsync(string agentName, string input, string systemPrompt)
        {
            // Register agent in dictionary if not already
            if (!_agents.ContainsKey(agentName))
            {
                _agents[agentName] = systemPrompt;
            }

            // Run the agent using RunAsync (current SDK)
            var response = await _client.RunAsync(
                project: _projectId,
                model: "gpt-4o-mini",
                input: input,
                instructions: _agents[agentName]
            );

            return response.Output;
        }
    }
}
