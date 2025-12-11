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
        private readonly PurchaseService _service  = service;

        [HttpPost("add")]
        public IActionResult AddPurchase(PurchaseDto dto)
        {
            _service.Add(dto);
            return Ok(new {message="Purchase Added"});
        }

        [HttpGet("all")]
        public IActionResult GetAllPurchases()
        {
            var all = _service.GetPurchase();
            return Ok(all);
        }
    }
}
