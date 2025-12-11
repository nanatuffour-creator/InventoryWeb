using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Entities;
using InventoryWeb.Services;

namespace InventoryWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("register")]
        public IActionResult AddUser([FromBody] UserEntities user)
        {
            if (user.Password != user.ConfirmPassword)
                return BadRequest(new { message = "Password must match ConfirmPassword" });

            _userService.AddUser(user);
            return Ok(new { message = "User Added" });
        }

        [HttpPost("login")]
        public IActionResult VerifyUser([FromBody] LoginEntities login)
        {
            var user = _userService.VerifyUser(login.Email!, login.Password!);

            if (user == null)
                return BadRequest(new { message = "Invalid email or password." });
            else if (user.Email != login.Email)
                return BadRequest(new { message = "Invalid email or password." });
            else if (user.Password != login.Password)
                return BadRequest(new { message = "Invalid email or password." });
            else if (user.Email != login.Email || user.Password != login.Password)
                return BadRequest(new { message = "Invalid email or password." });
            else
                return Ok(new { message = "Login Successful" });

        }
    }

}
