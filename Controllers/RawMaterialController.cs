using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "FactoryManager,WarehouseStaff")]
    public class RawMaterialController : ControllerBase
    {
        private readonly RawMaterialService _service;

        public RawMaterialController(RawMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterials()
        {
            var materials = await _service.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaterial([FromBody] RawMaterial material)
        {
            await _service.AddMaterialAsync(material);
            return Ok(material);
        }
    }
}