using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.Services;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;

namespace QuanLyMoiTruong.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiemQuanTracController : Controller
    {
        private readonly IDiemQuanTracService _DiemQuanTracService;
        public DiemQuanTracController(IDiemQuanTracService DiemQuanTracService)
        {
            _DiemQuanTracService = DiemQuanTracService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _DiemQuanTracService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(DiemQuanTracViewModel req)
        {
            if(req.IdDiemQuanTrac == 0)
            {
                var result = await _DiemQuanTracService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _DiemQuanTracService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10, string type="")
        {
            var param = new DiemQuanTracRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize,
                Type = type
            };
            var result = await _DiemQuanTracService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idDiemQuanTrac)
        {
            var result = await _DiemQuanTracService.Delete(idDiemQuanTrac);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idDiemQuanTrac)
        {
            var result = await _DiemQuanTracService.GetById(idDiemQuanTrac);
            return Ok(result);
        }
        [HttpGet("GetDuLieuCacDiemQuanTracLenBanDo")]
        public async Task<IActionResult> GetDuLieuCacDiemQuanTracLenBanDo(int idThanhPhanMoiTruong)
        {
            var result = await _DiemQuanTracService.GetDuLieuCacDiemQuanTracLenBanDo(idThanhPhanMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetDuLieuCacDiemXaThaiLenBanDo")]
        public async Task<IActionResult> GetDuLieuCacDiemXaThaiLenBanDo(string keyword, string loai)
        {
            var result = await _DiemQuanTracService.GetDuLieuCacDiemXaThaiLenBanDo(keyword, loai);
            return Ok(result);
        }
        [HttpGet("GetDanhSachLoaiDiemQuanTrac")]
        public async Task<IActionResult> GetDanhSachLoaiDiemQuanTrac()
        {
            var result = await _DiemQuanTracService.GetDanhSachLoaiDiemQuanTrac();
            return Ok(result);
        }
    }
}
