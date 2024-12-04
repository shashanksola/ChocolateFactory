using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "PackagingStaff,FactoryManager")]
    public class PackagingController : ControllerBase
    {
        private readonly PackagingService _service;

        public PackagingController(PackagingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFinishedGoods()
        {
            var goods = await _service.GetAllFinishedGoodsAsync();
            return Ok(goods);
        }

        [HttpPost]
        public async Task<IActionResult> AddFinishedGoodAsync([FromBody] FinishedGood finishedGood)
        {
            await _service.AddFinishedGoodAsync(finishedGood);
            return Ok(finishedGood);
        }
    }
}