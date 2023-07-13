using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Data.EF;
using QuanLyMoiTruong.Data.Entities;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace QuanLyMoiTruong.Application.Services
{
    public class FileService : IFileService
    {
        private readonly QuanLyMoiTruongDbContext _context;
        public IHttpContextAccessor _accessor;
        public FileService(QuanLyMoiTruongDbContext context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _accessor = HttpContextAccessor;
        }

        public async Task<ApiResult<List<int>>> Insert(FileUploadRequest req)
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("UserName")?.Value;
            var fullName = claimsIdentity.FindFirst("FullName")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            var entity = new Files();
            var result = new List<int>();
            foreach(var file in req.File)
            {
                var path = Path.Combine(req.NhomTaiLieu, req.IdTaiLieu.ToString());
                string filePath = await UploadFile(file, path);
                if (!String.IsNullOrEmpty(filePath))
                {
                    string fileName = file.FileName;
                    entity = new Files
                    {
                        LinkFile = filePath,
                        TenFile = fileName,
                        NgayTao = DateTime.Now,
                        NguoiTao = userName + " - " + fullName,
                        IdNguoiTao = userId
                    };
                }
                _context.Files.Add(entity);
                await _context.SaveChangesAsync();
                result.Add(entity.IdFile);
            }
            return new ApiSuccessResult<List<int>>() { Data = result };
        }
        public async static Task<string> UploadFile(IFormFile file, string path)
        {
            try
            {
                var folderName = "UploadFile";
                var pathToDB = Path.Combine(folderName, path);

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileNameOutput = fileName;
                var extension = Path.GetExtension(fileName).ToLower();

                if (!Directory.Exists(pathToDB))
                {
                    Directory.CreateDirectory(pathToDB);
                }

                var newFileName = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString() + "_" + RemoveSpecialCharacters(CommonUtils.RemoveSign4VietnameseString(fileName)).Replace("-", "");
                var newFilePath = Path.Combine(pathToDB, newFileName);
                using (Stream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine(pathToDB, newFileName);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        public async Task<ApiResult<bool>> Delete(int idFile)
        {
            var result = false;
            var data = _context.Files.FirstOrDefault(x => x.IdFile == idFile);
            if (data != null)
            {
                _context.Files.Remove(data);
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
