using InventoryWeb.Dto;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Category(Categories categories) : ControllerBase
    {
        private readonly Categories _categories = categories;

        [HttpPost("add")]
        public IActionResult AddCategory([FromBody] CategoryDto dto)
        {
            _categories.AddCategory(dto);
            return Ok(new { message = "Category Added" });
        }

        [HttpGet("all")]
        public IActionResult GetCategories()
        {
            var all = _categories.GetCategories();
            return Ok(all);
        }

        [HttpDelete("delete/{catName}")]
        public IActionResult Delete(string catName)
        {
            var all = _categories.DeleteCategory(catName);
            return Ok(new { message = "Deleted Sucessfully" });
        }
    }
}
