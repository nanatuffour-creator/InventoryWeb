using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(CustomerService services) : ControllerBase
    {
        private readonly CustomerService _customerServices = services;

        [HttpPost("add")]
        public IActionResult AddCustomer(Customer cus)
        {
            _customerServices.AddCustomer(cus);
            return Ok(new { message = "Customer Added Successfully" });
        }

        [HttpGet("all")]
        public IActionResult GetCustomers()
        {
            var all = _customerServices.GetCustomer();
            return Ok(all);
        }

        [HttpPut("edit/{name}")]
        public IActionResult EditCustomer(CustomerDto dto, string name)
        {
            _customerServices.UpdateCustomer(dto, name);
            return Ok(new { message = "Customer Edited Successfully" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveCustomer(int id)
        {
            _customerServices.DeleteCustomer(id);
            return Ok(new { message = "Customer Deleted Successfully" });
        }
    }
}
