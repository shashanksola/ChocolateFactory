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

        [HttpGet("/InProgress")]
        public async Task<IActionResult> GetAllActiveSchedules()
        {
            var schedules = await _service.GetAllSchedulesAsync();
            schedules = schedules.Where(x => x.Status == ProductionStatus.InProgress);
            return Ok(schedules);
        }

        [HttpGet("/Completed")]
        [Authorize(Roles ="QualityController")]
        public async Task<IActionResult> GetAllCompletedSchedules()
        {
            var schedules = await _service.GetAllSchedulesAsync();
            schedules = schedules.Where(x => x.Status == ProductionStatus.Completed);
            return Ok(schedules);
        }


        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] ProductionSchedule schedule)
        {
            await _service.AddScheduleAsync(schedule);
            return Ok(schedule);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CompleteProductionByScheduleID(Guid id)
        {
            var schedule = await _service.GetScheduleByIDAsync(id);

            schedule.Status = ProductionStatus.Completed;

            await _service.UpdateScheduleAsync(schedule);

            return Ok(schedule);
        }
    }
}