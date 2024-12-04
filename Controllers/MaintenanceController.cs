using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly MaintenanceService _service;

        public MaintenanceController(MaintenanceService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles ="Technician")]
        public async Task<IActionResult> GetAllMaintenanceRecords()
        {
            var records = await _service.GetAllMaintenanceRecordsAsync();
            return Ok(records);
        }

        [HttpPost]
        [Authorize(Roles ="Technician")]
        public async Task<IActionResult> AddMaintenanceRecordAsync([FromBody] MaintenanceRecord record)
        {
            await _service.AddMaintenanceRecordAsync(record);
            return Ok(record);
        }

        [HttpPut]
        [Authorize(Roles ="FactoryManager")]
        public async Task<IActionResult> UpdateMaintenenceRecordAsync([FromBody] MaintenanceRecord record)
        {
            var rec = _service.GetMaintenanceRecordByIdAsync(record.RecordId);

            if (rec == null) {
                return BadRequest("No Record Exists");
            }

            await _service.UpdateMaintenenceRecordAsync(record);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "FactoryManager")]
        public async Task<IActionResult> DeleteMaintenenceRecordAsync(Guid id)
        {
            var rec = _service.GetMaintenanceRecordByIdAsync(id);

            if (rec == null)
            {
                return BadRequest("No Record Exists");
            }

            await _service.DeleteMaintaneneceTask(id);
            return NoContent();
        }
    }
}