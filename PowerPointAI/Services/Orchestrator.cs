namespace PowerpointAi.Services
{
    public class OrchestratorService
    {
        private readonly FoundryRestService _foundry;

        public OrchestratorService(FoundryRestService foundry)
        {
            _foundry = foundry;
        }

        public async Task<string> RunPipelineAsync(string topic)
        {
            var research = await _foundry.RunAgentAsync("ResearchAgent", topic);
            var summary = await _foundry.RunAgentAsync("SummarizerAgent", research);
            return summary;
        }

        public async Task<string> RunResearchAsync(string topic)
        {
            return await _foundry.RunAgentAsync("ResearchAgent", topic);
        }

        public async Task<string> RunSummarizerAsync(string input)
        {
            return await _foundry.RunAgentAsync("SummarizerAgent", input);
        }
    }
}
