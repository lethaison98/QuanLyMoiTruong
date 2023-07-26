using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Claims;

namespace QuanLyMoiTruong.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public IHttpContextAccessor _accessor { get; set; }
        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IConfiguration config, IHttpContextAccessor HttpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _accessor = HttpContextAccessor;
        }
        public UserViewModel MapEntityToViewModel(AppUser entity)
        {
            var result = new UserViewModel();
            result.IdUser = entity.Id;
            result.UserName = entity.UserName;
            result.FullName = entity.FullName;
            result.Email = entity.Email;
            result.Birthday = entity.Birthday != null ? entity.Birthday.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            return result;
        }
        public AppUser MapViewModelToEntity(UserViewModel viewModel)
        {
            var entity = new AppUser();
            entity.Id = viewModel.IdUser;
            entity.UserName = viewModel.UserName;
            entity.FullName = viewModel.FullName;
            entity.Email = viewModel.Email;
            entity.Birthday = string.IsNullOrEmpty(viewModel.Birthday) ? null : DateTime.Parse(viewModel.Birthday, new CultureInfo("vi-VN"));
            return entity;
        }
        public async Task<ApiResult<bool>> InsertUser_Role(string userId, List<string> listRoleId)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var user = await _userManager.FindByNameAsync(userId);
            var result = await _userManager.AddToRolesAsync(user, listRoleId);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<ApiResult<bool>> InsertRoleClaims(string roleId, List<Claim> listClaims)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                foreach (var claim in listClaims)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }

                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }

        public async Task<ApiResult<IPagedList<UserViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<AppUser, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.FullName.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<AppUser>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<UserViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<UserViewModel>>() { Data = result };
        }

        public async Task<ApiResult<IList<UserViewModel>>> GetAll()
        {
            var result = new List<UserViewModel>();
            var entities = await _unitOfWork.GetRepository<AppUser>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<UserViewModel>>() { Data = result };
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var result = new UserViewModel();
            var data = await _unitOfWork.GetRepository<AppUser>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<UserViewModel>() { Data = result };
        }

        public async Task<ApiResult<AppUser>> Insert(UserViewModel obj)
        {
            var usercheck = _userManager.FindByNameAsync(obj.UserName).Result;
            if (usercheck != null)
            {
                return new ApiErrorResult<AppUser>("Tài khoản đã tồn tại");
            }
            if (!String.IsNullOrEmpty(obj.Email) && await _userManager.FindByEmailAsync(obj.Email) != null)
            {
                return new ApiErrorResult<AppUser>("Email đã tồn tại");
            }
            var entity = MapViewModelToEntity(obj);
            var result = await _userManager.CreateAsync(entity, obj.Password);
            if (result.Succeeded)
            {
                //var u = await _userManager.FindByNameAsync(obj.UserName);
                //await _userManager.AddToRolesAsync(u, obj.DsRole);
                return new ApiSuccessResult<AppUser>();
            }
            else
            {
                return new ApiErrorResult<AppUser>("Đăng ký không thành công");
            }
        }

        public async Task<ApiResult<AppUser>> Update(UserViewModel obj)
        {
            var users = await _userManager.FindByIdAsync(obj.IdUser.ToString());
            if (users.UserName != obj.UserName)
            {
                var usercheck = _userManager.FindByNameAsync(obj.UserName).Result;
                if (usercheck != null)
                {
                    return new ApiErrorResult<AppUser>("Tài khoản đã tồn tại");
                }
            }
            if (!String.IsNullOrEmpty(obj.Email) && users.Email != obj.Email)
            {
                var usercheck = await _userManager.FindByEmailAsync(obj.Email);
                if (usercheck != null)
                {
                    return new ApiErrorResult<AppUser>("Email đã tồn tại");
                }
            }

            users.FullName = obj.FullName;
            users.Birthday = DateTime.Now;
            users.PhoneNumber = obj.PhoneNumber;
            var result = await _userManager.UpdateAsync(users);
            if (result.Succeeded)
            {
                //var currentRoles = await _userManager.GetRolesAsync(users);
                //var u = await _userManager.FindByNameAsync(obj.UserName);
                //await _userManager.RemoveFromRolesAsync(users, currentRoles);
                //await _userManager.AddToRolesAsync(u, obj.DsRole);
                return new ApiSuccessResult<AppUser>();
            }
            else
            {
                return new ApiErrorResult<AppUser>("Đăng ký không thành công");
            }
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<AppUser>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<bool>> Remove(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<AppUser>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<AppUser>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }
    }
}
