using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(InvoiceService invoiceService) : ControllerBase
    {
        private readonly InvoiceService _invoiceService = invoiceService;

        [HttpGet("all")]
        public IActionResult All()
        {
            var all = _invoiceService.GetInvoices();
            return Ok(all);
        }

        [HttpPost("new")]
        public IActionResult CreateInvoice([FromBody] InvoiceRequestDto request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return BadRequest("Invalid invoice data");

            _invoiceService.CreateInvoice(request);

            return Ok(new
            {
                message = "Invoice Added Sucessfully"
            });
        }

        [HttpGet("total")]
        public IActionResult GetTotal()
        {
            var total = _invoiceService.GetInvoicesTotal();
            return Ok(total);
        }

        [HttpGet("percentage")]
        public IActionResult GetTotalPercentage()
        {
            var total = _invoiceService.GetInvoicesTotalByDate();
            return Ok(total);
        }

        [HttpGet("today")]
        public IActionResult GetTotalForToday()
        {
            var total = _invoiceService.GetInvoicesTotalForToday();
            return Ok(total);
        }
    }
}
