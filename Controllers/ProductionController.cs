using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ProductionSupervisor,FactoryManager")]
    public class ProductionController : ControllerBase
    {
        private readonly ProductionService _service;

        public ProductionController(ProductionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _service.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] ProductionSchedule schedule)
        {
            await _service.AddScheduleAsync(schedule);
            return Ok(schedule);
        }
    }
}