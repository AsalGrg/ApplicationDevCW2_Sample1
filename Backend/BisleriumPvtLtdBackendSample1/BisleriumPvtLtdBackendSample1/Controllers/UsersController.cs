using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BisleriumPvtLtdBackendSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            LoginUserResponse response = _userService.LoginUser(loginUserDto);

            if (response == null) return NotFound("Invalid Credentials");
            else return Ok(response);
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto) {
            User user = _userService.RegisterUser(registerUserDto);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
