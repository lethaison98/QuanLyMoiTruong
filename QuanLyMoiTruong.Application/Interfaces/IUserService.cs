using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModel;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IUserService : IBaseService<AppUser, Guid, UserViewModel, PagingRequest>
    {
        public Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims);
        public Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoles);
    }
}
