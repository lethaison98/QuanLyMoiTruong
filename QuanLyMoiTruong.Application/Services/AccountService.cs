using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.EF;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly QuanLyMoiTruongDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private IHttpContextAccessor _accessor { get; set; }
        public AccountService(QuanLyMoiTruongDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor HttpContextAccessor, ILogger<AccountService> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _accessor = HttpContextAccessor;
            _logger = logger;
        }
        public async Task<ApiResult<AccountViewModel>> Login(LoginViewModel loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user == null) return new ApiErrorResult<AccountViewModel>("Tài khoản không tồn tại!"); ;

            var login = await _signInManager.PasswordSignInAsync(user, loginVM.PassWord, loginVM.RememberMe, true);
            if (!login.Succeeded)
            {
                return new ApiErrorResult<AccountViewModel>("Mật khẩu không chính xác, vui lòng kiểm tra lại!");
            }
            _logger.LogWarning(user.UserName + " - " + user.FullName + " Đăng nhập thành công từ IP " + _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            var info = new[]
            {
                new Claim("UserName", user.UserName.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("FullName", user.FullName.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Issuer"],
                claims: info,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var result = new AccountViewModel();
            result.Token = new JwtSecurityTokenHandler().WriteToken(token);
            result.UserName = user.UserName;
            result.FullName = user.FullName;
            result.Roles = string.Join(';', roles);
            result.Claims = string.Join(';', claims);
            return new ApiSuccessResult<AccountViewModel>(result);
        }

        public async Task<ApiResult<bool>> Logout()
        {
            await _signInManager.SignOutAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Register(RegisterViewModel registerVM)
        {
            var user = await _userManager.FindByNameAsync(registerVM.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(registerVM.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            user = new AppUser()
            {
                Birthday = registerVM.Birthday,
                Email = registerVM.Email,
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                PhoneNumber = registerVM.PhoneNumber,
                CreateDate = DateTime.Now,
                Status = 1,
                Level = 1,
                IsAdmin = true,
                IsDeleted = false,
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }
        public async Task<ApiResult<bool>> ChangePassByUser(ChangePasswordViewModel changePasswordVM)
        {
            try
            {
                var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst("UserId")?.Value;
                var user = await _userManager.FindByIdAsync(userId);
                var x = new object();
                if (changePasswordVM.NewPassword != null && changePasswordVM.OldPassword != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword, changePasswordVM.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);
                        return new ApiSuccessResult<bool>() { };
                    }
                    else
                    {
                        return new ApiErrorResult<bool>(changePasswordResult.Errors.First().Description) { };
                    }
                }
                return new ApiErrorResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);

            }

        }
    }
}
