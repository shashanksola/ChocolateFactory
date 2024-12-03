using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "QualityController,FactoryManager")]
    public class QualityController : ControllerBase
    {
        private readonly QualityControlService _service;

        public QualityController(QualityControlService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChecks()
        {
            var checks = await _service.GetAllChecksAsync();
            return Ok(checks);
        }

        [HttpPost]
        public async Task<IActionResult> AddQualityCheck([FromBody] QualityCheck check)
        {
            await _service.AddQualityCheckAsync(check);
            return Ok(check);
        }
    }
}