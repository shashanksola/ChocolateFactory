using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "FactoryManager,MaintenanceStaff")]
    public class MaintenanceController : ControllerBase
    {
        private readonly MaintenanceService _service;

        public MaintenanceController(MaintenanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaintenanceRecords()
        {
            var records = await _service.GetAllMaintenanceRecordsAsync();
            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaintenanceRecordAsync([FromBody] MaintenanceRecord record)
        {
            await _service.AddMaintenanceRecordAsync(record);
            return Ok(record);
        }
    }
}