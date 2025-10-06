using PowerpointAi.Models;

namespace PowerpointAi.Services
{
    public class Orchestrator
    {
        private readonly AgentService _agentService;
        private readonly PowerPointService _powerPointService;

        public Orchestrator(AgentService agentService, PowerPointService powerPointService)
        {
            _agentService = agentService;
            _powerPointService = powerPointService;
        }

        public async Task<List<McpResponse>> OrchestrateAsync(string userQuery)
        {
            var results = new List<McpResponse>();

            // Step 1: Research
            var research = await _agentService.RunAgentAsync(
                new McpMessage { Agent = "ResearchAgent", Input = userQuery },
                new AgentConfig { Name = "ResearchAgent", Prompt = "You are a research assistant." });
            results.Add(research);

            // Step 2: Summarizer
            var summary = await _agentService.RunAgentAsync(
                new McpMessage { Agent = "SummarizerAgent", Input = research.Output },
                new AgentConfig { Name = "SummarizerAgent", Prompt = "Summarize into JSON with title + key points." });
            results.Add(summary);

            // Step 3: PowerPoint Generation
            string pptPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPresentation.pptx");
            _powerPointService.GeneratePpt(summary, pptPath);

            results.Add(new McpResponse
            {
                Agent = "PresenterAgent",
                Output = $"Presentation generated at: {pptPath}"
            });

            return results;
        }
    }
}
