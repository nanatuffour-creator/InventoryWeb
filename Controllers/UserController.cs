using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Entities;
using InventoryWeb.Services;
using InventoryWeb.Dto;
using Microsoft.AspNetCore.Authorization;

namespace InventoryWeb.Controllers
{
    [Authorize]
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
        public IActionResult VerifyUser([FromBody] LoginEntities login)
        {
            var user = _userService.VerifyUser(login.Email!, login.Password!);

            if (user == null)
                return BadRequest(new { message = "Invalid email or password." });
            else if (user.Email != login.Email)
                return BadRequest(new { message = "Invalid email." });
            else if (user.Password != login.Password)
                return BadRequest(new { message = "Invalid password." });
            else
                return Ok(new { message = "Login Successful" });

        }
    }

}
