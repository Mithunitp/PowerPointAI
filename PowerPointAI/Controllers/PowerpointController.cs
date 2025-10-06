using Microsoft.AspNetCore.Mvc;
using PowerpointAi.Services;

namespace PowerpointAi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerpointController : ControllerBase
    {
        private readonly Orchestrator _orchestrator;

        public PowerpointController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [HttpPost("run")]
        public async Task<IActionResult> Run([FromBody] string query)
        {
            var results = await _orchestrator.OrchestrateAsync(query);
            return Ok(results);
        }

        [HttpGet("download")]
        public IActionResult Download()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPresentation.pptx");
            if (!System.IO.File.Exists(filePath))
                return NotFound("Run /run first to generate PPT.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.presentationml.presentation", "GeneratedPresentation.pptx");
        }
    }
}
