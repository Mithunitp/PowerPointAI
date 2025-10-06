using Azure.AI.Projects;

namespace PowerpointAi.Services
{
    public class FoundryService
    {
        private readonly AIProjectClient _client;
        private readonly string _projectId;

        public FoundryService(AIProjectClient client, IConfiguration config)
        {
            _client = client;
            _projectId = config["AzureAI:ProjectId"]!;
        }

        public async Task<string> RunAgentAsync(string agentName, string input, string systemPrompt)
        {
            var agentsClient = _client.GetPersistentAgentsClient();

            var existing = agentsClient.ListAgents(_projectId)
                                       .FirstOrDefault(a => a.Name == agentName);

            if (existing is null)
            {
                var created = await agentsClient.CreateAgentAsync(
                    project: _projectId,
                    name: agentName,
                    instructions: systemPrompt,
                    model: "gpt-4o-mini"
                );
                existing = created.Value;
            }

            var run = await agentsClient.RunAgentAsync(
                project: _projectId,
                agentName: existing.Name,
                input: input
            );

            return run.Value.Output;
        }
    }
}
