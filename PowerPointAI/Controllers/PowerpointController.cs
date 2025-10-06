using Microsoft.AspNetCore.Mvc;
using PowerpointAi.Models;
using PowerpointAi.Services;

namespace PowerpointAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly OrchestratorService _orchestrator;

        public DemoController(OrchestratorService orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("run")]
        public async Task<IActionResult> RunPipeline([FromBody] PresentationRequest request)
        {
            var summary = await _orchestrator.RunPipelineAsync(request.Topic);
            return Ok(new { Summary = summary });
        }
    }
}
