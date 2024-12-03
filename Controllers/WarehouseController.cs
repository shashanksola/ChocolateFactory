using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;
using System.ComponentModel.DataAnnotations;
using ChocolateFactory.Requests;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "WarehouseStaff,FactoryManager")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _service;

        public WarehouseController(WarehouseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _service.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouseAsync([FromBody] WarehouseRequest warehouse)
        {

            var warehouses = await _service.GetAllWarehousesAsync();

            var alreadyExists = warehouses.Find(x=>x.Name == warehouse.Name);

            if (alreadyExists != null) {
                return BadRequest("Warehouse name already exists, please use unique names");
            }

            Warehouse temp = new Warehouse
            {
                Location = warehouse.Location,
                Name = warehouse.Name,
                Capacity = warehouse.Capacity,
                ManagerId = warehouse.ManagerId,
                CurrentStockLevel = warehouse.CurrentStockLevel
            };

            await _service.AddWarehouseAsync(temp);
            return Ok(warehouse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWarehouseAsync(Warehouse warehouse)
        {
            bool exists = await _service.WarehouseWithNameExistsAsync(warehouse.Name);

            if (exists == true)
            {
                return BadRequest("Warehouse with Name: " + warehouse.Name + " doesn't exist");
            }

            await _service.UpdateWarehouseAsync(warehouse);
            return Ok("Warehouse Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouseAsync(Warehouse warehouse)
        {
            bool exists = await _service.WarehouseWithNameExistsAsync(warehouse.Name);

            if (exists == true)
            {
                return BadRequest("Warehouse with Name: " + warehouse.Name + " doesn't exist");
            }

            await _service.DeleteWarehouseAsync(warehouse);
            return Ok("Warehouse Updated Successfully");
        }
    }
}