using Microsoft.AspNetCore.Mvc;
using PowerpointAi.Models;
using PowerpointAi.Services;

namespace PowerpointAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResearchAgentController : ControllerBase
    {
        private readonly OrchestratorService _orchestrator;

        public ResearchAgentController(OrchestratorService orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("run")]
        public async Task<IActionResult> RunResearch([FromBody] PresentationRequest request)
        {
            var output = await _orchestrator.RunResearchAsync(request.Topic);
            return Ok(new { ResearchOutput = output });
        }
    }
}
