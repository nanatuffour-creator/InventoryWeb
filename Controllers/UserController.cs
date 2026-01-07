using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using InventoryWeb.Dto;
using Microsoft.AspNetCore.Authorization;

namespace InventoryWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("register")]
        public IActionResult AddUser([FromBody] UserDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { message = "Password must match ConfirmPassword" });

            _userService.AddUser(dto);
            return Ok(new { message = "User Added" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUser([FromBody] LoginDto login)
        {
            var user = await _userService.VerifyUser(login);
            return Ok(new {message = user});

        }
    }

}
