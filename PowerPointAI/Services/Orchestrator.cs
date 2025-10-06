namespace PowerpointAi.Services
{
    public class OrchestratorService
    {
        private readonly FoundryService _foundry;

        public OrchestratorService(FoundryService foundry)
        {
            _foundry = foundry;
        }

        public async Task<string> RunPipelineAsync(string topic)
        {
            // Step 1: Research
            var research = await _foundry.RunAgentAsync(
                "ResearchAgent",
                topic,
                "You are a research assistant. Gather detailed points on the given topic."
            );

            // Step 2: Summarize
            var summary = await _foundry.RunAgentAsync(
                "SummarizerAgent",
                research,
                "You are a summarizer. Turn the research input into a concise, clear summary."
            );

            return summary;
        }

        public async Task<string> RunResearchAsync(string topic)
        {
            return await _foundry.RunAgentAsync(
                "ResearchAgent",
                topic,
                "You are a research assistant. Gather detailed points on the given topic."
            );
        }

        public async Task<string> RunSummarizerAsync(string input)
        {
            return await _foundry.RunAgentAsync(
                "SummarizerAgent",
                input,
                "You are a summarizer. Turn the research input into a concise, clear summary."
            );
        }
    }
}
