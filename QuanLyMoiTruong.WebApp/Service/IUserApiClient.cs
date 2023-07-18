using QuanLyMoiTruong.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.WebApp.Service
{
    public interface IUserApiClient
    {
        Task<ApiResult<AccountViewModel>> Authenticate(LoginViewModel request);
    }
}