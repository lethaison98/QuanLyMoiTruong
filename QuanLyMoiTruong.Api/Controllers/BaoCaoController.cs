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
        private readonly IKetQuaBaoVeMoiTruongDoanhNghiepService _ketQuaBaoVeMoiTruongDoanhNghiepService;
        private readonly IKetQuaBaoVeMoiTruongKCNService _ketQuaBaoVeMoiTruongKCNService;
        public BaoCaoController(IBaoCaoThongKeNguonThaiService baoCaoThongKeNguonThaiService, IGiayPhepMoiTruongService giayPhepMoiTruongService, IKetQuaBaoVeMoiTruongDoanhNghiepService ketQuaBaoVeMoiTruongDoanhNghiepService, IKetQuaBaoVeMoiTruongKCNService ketQuaBaoVeMoiTruongKCNService)
        {
            _baoCaoThongKeNguonThaiService = baoCaoThongKeNguonThaiService;
            _giayPhepMoiTruongService = giayPhepMoiTruongService;
            _ketQuaBaoVeMoiTruongDoanhNghiepService = ketQuaBaoVeMoiTruongDoanhNghiepService;
            _ketQuaBaoVeMoiTruongKCNService = ketQuaBaoVeMoiTruongKCNService;

        }
        [HttpPost("BaoCaoCapGiayPhepMoiTruong")]
        public async Task<IActionResult> GetListByKhoangThoiGian(GiayPhepMoiTruongRequest request)
        {
            var result = await _giayPhepMoiTruongService.GetListByKhoangThoiGian(request);
            return Ok(result);
        }
        [HttpPost("BaoCaoDanhSachDoanhNghiepHoatDongTrongCacKCN")]
        public async Task<IActionResult> GetListKetQuaBaoVeMoiTruongDoanhNghiep(BaoCaoBaoVeMoiTruongRequest request)
        {
            var result = await _ketQuaBaoVeMoiTruongDoanhNghiepService.GetAll();
            return Ok(result);
        }
        [HttpGet("BaoCaoMoiTruongCacKCN")]
        public async Task<IActionResult> GetListKetQuaBaoVeMoiTruongKCN(BaoCaoBaoVeMoiTruongRequest request)
        {
            var result = await _ketQuaBaoVeMoiTruongKCNService.GetAll();
            return Ok(result);
        }
    }
}
