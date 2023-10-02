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
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoThongKeNguonThaiService _baoCaoThongKeNguonThaiService;
        private readonly IGiayPhepMoiTruongService _giayPhepMoiTruongService;
        public BaoCaoController(IBaoCaoThongKeNguonThaiService baoCaoThongKeNguonThaiService, IGiayPhepMoiTruongService giayPhepMoiTruongService)
        {
            _baoCaoThongKeNguonThaiService = baoCaoThongKeNguonThaiService;
            _giayPhepMoiTruongService = giayPhepMoiTruongService;

        }
        [HttpPost("BaoCaoCapGiayPhepMoiTruong")]
        public async Task<IActionResult> GetListByKhoangThoiGian(GiayPhepMoiTruongRequest request)
        {
            var result = await _giayPhepMoiTruongService.GetListByKhoangThoiGian(request);
            return Ok(result);
        }
    }
}
