using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController(SupplierService supplierService) : ControllerBase
    {
        private readonly SupplierService _supplierService = supplierService;

        [HttpPost("add")]
        public IActionResult AddSupplier(SuppliersDto dto)
        {
            _supplierService.AddSupplier(dto);
            return Ok(new { message = "Supplier Added." });
        }

        [HttpGet("all")]
        public IActionResult GetAllSuppliers()
        {
            var all = _supplierService.GetSuppliers();
            return Ok(all);
        }

        [HttpDelete("delete/{supplierName}")]
        public IActionResult Delete(string supplierName)
        {
            _supplierService.Deletesupplier(supplierName);
            return Ok(new { message = "Supplier Deleted" });
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, [FromBody] SupplierDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "Supplier ID mismatch" });

            var success = _supplierService.EditSupplier(dto);

            if (!success)
                return BadRequest(new { message = "Supplier not found " });

            return Ok(new { message = "Supplier updated" });
        }
    }
}
