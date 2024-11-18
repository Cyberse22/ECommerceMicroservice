using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<ApplicationUser> userManager, IAccountService accountService, IMapper mapper)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _accountService.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _accountService.SignInAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            if (model == null)
            {
                return BadRequest("Error");
            }
            var currentUser = await _accountService.GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }
            var email = currentUser.Email;
            if(string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }
            var result = await _accountService.ChangePasswordAsync(email, model.CurrentPassword, model.NewPassword);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
