using QuanLyMoiTruong.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<ApiResult<AccountViewModel>> Login(LoginViewModel loginVM);
        public Task<ApiResult<bool>> Logout();
        public Task<ApiResult<bool>> Register(RegisterViewModel registerVM);
        public Task<ApiResult<bool>> ChangePassByUser(ChangePasswordViewModel changePasswordVM);

    }
}
