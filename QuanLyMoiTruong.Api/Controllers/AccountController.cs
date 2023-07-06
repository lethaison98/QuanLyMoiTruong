using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.ViewModels;

namespace QuanLyMoiTruong.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Register(viewModel);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _logger.LogCritical("Vào trang Index");

            var result = await _accountService.Login(viewModel);
            return Ok(result);
        }
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Logout();
            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.ChangePassByUser(viewModel);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
