
using Microsoft.AspNetCore.Mvc;
using PowerpointAi.Models;
using PowerpointAi.Services;

namespace PowerpointAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummarizerAgentController : ControllerBase
    {
        private readonly OrchestratorService _orchestrator;

        public SummarizerAgentController(OrchestratorService orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("run")]
        public async Task<IActionResult> RunSummarizer([FromBody] PresentationRequest request)
        {
            var output = await _orchestrator.RunSummarizerAsync(request.Topic);
            return Ok(new { SummaryOutput = output });
        }
    }
}
