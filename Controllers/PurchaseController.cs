using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController(PurchaseService service) : ControllerBase
    {
        private readonly PurchaseService _service = service;

        [HttpPost("add")]
        public IActionResult AddPurchase([FromBody] PurchaseDto dto)
        {
            _service.Add(dto);
            return Ok(new { message = "Purchase Added" });
        }

        [HttpGet("all")]
        public IActionResult GetAllPurchases()
        {
            var all = _service.GetPurchase();
            return Ok(all);
        }

        [HttpGet("completedpurchase")]
        public IActionResult GetAllPurchasesCompleted()
        {
            var all = _service.GetPurchasesCompleted();
            return Ok(all);
        }

        [HttpGet("pendingpurchase")]
        public IActionResult GetAllPurchasesPending()
        {
            var all = _service.GetPurchasesPending();
            return Ok(all);
        }

        [HttpGet("delayedpurchase")]
        public IActionResult GetAllPurchasesDelayed()
        {
            var all = _service.GetPurchasesDelayed();
            return Ok(all);
        }

        [HttpGet("total")]
        public Decimal GetTotalPurchase()
        {
            return _service.GetTotalPurchase();
        }

        [HttpGet("pending")]
        public int GetPendingPurchases()
        {
            return _service.GetPendingOrders();
        }

        [HttpGet("completed")]
        public int GetCompletedPurchases()
        {
            return _service.GetCompletedOrders();
        }
    }
}
