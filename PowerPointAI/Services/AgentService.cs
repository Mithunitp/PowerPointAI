using PowerpointAi.Models;

namespace PowerpointAi.Services
{
    public class AgentService
    {
        private readonly FoundryClientWrapper _foundryClient;

        public AgentService(FoundryClientWrapper foundryClient)
        {
            _foundryClient = foundryClient;
        }

        public async Task<McpResponse> RunAgentAsync(McpMessage msg, AgentConfig config)
        {
            // Optionally, use RAG retrieval
            // var docs = await _foundryClient.RetrieveDocumentsAsync(msg.Input);
            // var promptWithContext = msg.Input + "\n\nContext:\n" + string.Join("\n", docs);

            var output = await _foundryClient.RunAgentAsync(config.Name, msg.Input);

            return new McpResponse
            {
                Agent = config.Name,
                Output = output
            };
        }
    }
}
