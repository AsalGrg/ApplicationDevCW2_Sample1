using BisleriumPvtLtdBackendSample1.DTOs;
using BisleriumPvtLtdBackendSample1.DTOs.Blog;
using BisleriumPvtLtdBackendSample1.Models;
using BisleriumPvtLtdBackendSample1.ServiceInterfaces;
using BisleriumPvtLtdBackendSample1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomUserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;

        public CustomUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            , RoleManager<IdentityRole> roleManager,
            EmailService emailService,
            IUserService userService,
            IBlogService blogService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _userService = userService;
            _blogService = blogService;
        }


        [HttpGet]
        [Route("user-details")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok(await _userService.GetCompleteUserDetails());   
        }


        [HttpPut]
        [Route("updateUserDetails")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails([FromBody] RegisterUserDto registerUserDto)
        {
            var userDetails = await _userManager.FindByIdAsync(registerUserDto.Id);

            userDetails.UserName = registerUserDto.Username;
            userDetails.PhoneNumber = registerUserDto.PhoneNumber;

            var process = await _userManager.UpdateAsync(userDetails);

            if (process.Succeeded)
            {
                return Ok();
            }
            else { 

            return BadRequest();
                }
           
        }



        [HttpDelete]
        [Route("deleteUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var userDetails = await _userManager.FindByIdAsync(userId);

            if (userDetails!=null)
            {
                var process = await _userManager.DeleteAsync(userDetails);
                if (process.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();

        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user!=null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Email already exists");
            }


            user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username already exists");
            }


            user = new IdentityUser();
            user.UserName = model.Username;
            user.Email = model.Email;
            user.EmailConfirmed = false;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Check if the default role exists, create it if not
                string defaultRoleName = "Blogger";
                var roleExists = await _roleManager.RoleExistsAsync(defaultRoleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(defaultRoleName));
                }

                // Assign the default role to the user
                await _userManager.AddToRoleAsync(user, defaultRoleName);

                var token =await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailVerificationLink = $"http://localhost:3006/verifyEmail?email={user.Email}&token={token}";
                _emailService.sendEmail(
                    "Registration Email Verification",
                    user.Email,
                    $"Please reset your password by clicking <a href='{emailVerificationLink}'>here</a>."
                    );
                return Ok();

                // Optionally sign in the user after registration
                // await _signInManager.SignInAsync(user, isPersistent: false
            }
            else
            {
                return BadRequest(result.Errors); // Return error response
            }
        }

        [HttpPost]
        [Route("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Email already exists");
            }


            user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username already exists");
            }


            user = new IdentityUser();
            user.UserName = model.Username;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Check if the default role exists, create it if not
                string defaultRoleName = "Admin";
                var roleExists = await _roleManager.RoleExistsAsync(defaultRoleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(defaultRoleName));
                }

                // Assign the default role to the user
                await _userManager.AddToRoleAsync(user, defaultRoleName);
                return Ok();

                // Optionally sign in the user after registration
                // await _signInManager.SignInAsync(user, isPersistent: false
            }
            else
            {
                return BadRequest(result.Errors); // Return error response
            }
        }

        [HttpPost]
        [Route("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {

                System.Diagnostics.Debug.WriteLine("GHEEEEE");
                var resetPassResult = await _userManager.ConfirmEmailAsync(user, model.Token);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                System.Diagnostics.Debug.WriteLine("GHEEEEE");
                return Ok("Email Confirmed Successfully");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Couldnot send link on your emal. Please try again!");
        }


        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email.ToUpper());
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = $"http://localhost:3006/forgotPassword/verify-email?email={user.Email}&token={token}";
                _emailService.sendEmail(
                    "Forgot Password Email Verification",
                    user.Email,
                    $"Please reset your password by clicking <a href='{forgotPasswordLink}'>here</a>."
                    );
                return Ok();
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Couldnot send link on your emal. Please try again!");
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            System.Diagnostics.Debug.WriteLine(model.ChangedPassword);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {

                System.Diagnostics.Debug.WriteLine("GHEEEEE");
                var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.ChangedPassword);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return Ok("Password Changed Successfully");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Couldnot send link on your emal. Please try again!");
        }


        [HttpPost]
        [Authorize]
        [Route("getAnalysis")]
        public IActionResult GetAllSelectedTimeAnalysis([FromBody]TimeAnalysisRequest analysisDetails)
        {

            return Ok(_blogService.GetTotalTimeAnalysis(analysisDetails));
        }
    }

}