using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.EF;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Services
{
    public class FileTaiLieuService : IFileTaiLieuService
    {
        private readonly QuanLyMoiTruongDbContext _context;
        private readonly IFileService _fileService;
        public IHttpContextAccessor _accessor;

        public FileTaiLieuService(QuanLyMoiTruongDbContext context, IFileService fileService, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _fileService = fileService;
            _accessor = HttpContextAccessor;
        }
        public async Task<ApiResult<int>> Insert(int idFile, int idTaiLieu, string nhomTaiLieu, string loaiFileTaiLieu)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var result = 0;
            var entity = new FileTaiLieu();
            entity = new FileTaiLieu
            {
                IdFile = idFile,
                IdTaiLieu = idTaiLieu,
                NhomTaiLieu = nhomTaiLieu,
                LoaiFileTaiLieu = loaiFileTaiLieu,
                NgayTao = DateTime.Now,
                NguoiTao = tenUser,
                IdNguoiTao = userId
            };
            _context.FileTaiLieu.Update(entity);
            await _context.SaveChangesAsync();
            result = entity.IdFileTaiLieu;
            return new ApiSuccessResult<int>() { Data = result };
        }
        public async Task<ApiResult<int>> UpdateAll(FileTaiLieuRequest request)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var tenUser = claimsIdentity.FindFirst("HoTen")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var result = 0;
            var idTaiLieu = request.IdTaiLieu;
            var nhomTaiLieu = request.NhomTaiLieu;
            // OldFile: Là những file đang có Id > 0
            var listIdOldFile = request.FileTaiLieu.Where(x => x.IdFileTaiLieu > 0).Select(x => x.IdFileTaiLieu); 
            
            // RemoveFile: Là những file được lấy ra theo IdTaiLieu và LoaiTaiLieu, nhưng không có trong danh sách OldFile nên sẽ bị xóa
            var listRemoveFile = _context.FileTaiLieu.Where(x => x.IdTaiLieu == idTaiLieu && x.NhomTaiLieu == nhomTaiLieu && !listIdOldFile.Contains(x.IdFileTaiLieu));
            foreach (var item in listRemoveFile)
            {
                item.TrangThai = 4;
            }
            _context.FileTaiLieu.UpdateRange(listRemoveFile);
            await _context.SaveChangesAsync();

            // NewFile: Là những file có Id = 0
            var listNewFile = request.FileTaiLieu.Where(x => x.IdFileTaiLieu == 0);
            foreach (var item in listNewFile)
            {
                item.NgayTao = DateTime.Now;
                item.NguoiTao = tenUser;
                item.IdNguoiTao = userId;
            }
            _context.FileTaiLieu.AddRange(listNewFile);
            await _context.SaveChangesAsync();
            result = 1;
            return new ApiSuccessResult<int>() { Data = result };
        }

        public async Task<ApiResult<bool>> Delete(int idFileTaiLieu)
        {
            var result = false;
            var data = _context.FileTaiLieu.Include(x => x.File).FirstOrDefault(x => x.IdFile == idFileTaiLieu);
            if (data != null)
            {
                _context.FileTaiLieu.Remove(data);
                await _context.SaveChangesAsync();
                result = true;
                return new ApiSuccessResult<bool>() { Data = result };
            }
            else
            {
                result = false;
                return new ApiErrorResult<bool>() { Data = result };
            }

        }
    }
}
