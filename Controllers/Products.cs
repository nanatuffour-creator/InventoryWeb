using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products(ProductServices productServices) : ControllerBase
    {
        private readonly ProductServices _productServices = productServices;
        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] ProductsDto dto)
        {
            _productServices.AddProduct(dto);
            return Ok(new { message = "Product added successfully" });
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productServices.GetProducts();
            return Ok(products);
        }

        [HttpDelete("delete/{productName}")]
        public IActionResult DeleteProduct(string productName)
        {
            _productServices.DeleteProduct(productName);
            return Ok(new { message = "Product deleted successfully" });
        }

        // [HttpPut("edit")]
        // public IActionResult EditProduct([FromBody] ProductsDto dto)
        // {
        //     _productServices.UpdateProduct(dto);
        //     return Ok(new { message = "Product updated successfully" });
        // }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            if (id != dto.ProductId)
                return BadRequest(new { message = "Product ID mismatch" });

            var success = _productServices.UpdateProduct(dto);

            if (!success)
                return BadRequest(new { message = "Product not found or invalid category" });

            return Ok(new { message = "Product updated" });
        }





    }
}
