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
    public class BaoCaoBaoVeMoiTruongController : Controller
    {
        private readonly IBaoCaoBaoVeMoiTruongService _BaoCaoBaoVeMoiTruongService;
        private readonly IKetQuaBaoVeMoiTruongDoanhNghiepService _ketQuaBaoVeMoiTruongDoanhNghiepService;
        private readonly IKetQuaBaoVeMoiTruongKCNService _ketQuaBaoVeMoiTruongKCNService;
        public BaoCaoBaoVeMoiTruongController(IBaoCaoBaoVeMoiTruongService BaoCaoBaoVeMoiTruongService, IKetQuaBaoVeMoiTruongDoanhNghiepService ketQuaBaoVeMoiTruongDoanhNghiepService, IKetQuaBaoVeMoiTruongKCNService ketQuaBaoVeMoiTruongKCNService)
        {
            _BaoCaoBaoVeMoiTruongService = BaoCaoBaoVeMoiTruongService;
            _ketQuaBaoVeMoiTruongDoanhNghiepService = ketQuaBaoVeMoiTruongDoanhNghiepService;
            _ketQuaBaoVeMoiTruongKCNService = ketQuaBaoVeMoiTruongKCNService;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoBaoVeMoiTruongViewModel req)
        {
            if(req.IdBaoCaoBaoVeMoiTruong == 0)
            {
                var result = await _BaoCaoBaoVeMoiTruongService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _BaoCaoBaoVeMoiTruongService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoBaoVeMoiTruongRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _BaoCaoBaoVeMoiTruongService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.Delete(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetById(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTHangNamByDuAn")]
        public async Task<IActionResult> GetGPMTByDuAn(int idDuAn)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetListBaoCaoBaoVeMoiTruongByDuAn(idDuAn);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTHangNamByKhuCongNghiep")]
        public async Task<IActionResult> GetGPMTByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetListBaoCaoBaoVeMoiTruongByKhuCongNghiep(idKhuCongNghiep);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTHangNamByKhuKinhTe")]
        public async Task<IActionResult> GetGPMTByKhuKinhTe()
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetListBaoCaoBaoVeMoiTruongByKhuKinhTe();
            return Ok(result);
        }
        [HttpGet("GetDoanhNghiepHoatDongTrongKCNByBaoCao")]
        public async Task<IActionResult> GetDoanhNghiepHoatDongTrongKCNByBaoCao(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _ketQuaBaoVeMoiTruongDoanhNghiepService.GetAllByIdBaoCaoBaoVeMoiTruong(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetChiTieuBaoVeMoiTruongKCNByBaoCao")]
        public async Task<IActionResult> GetChiTieuBaoVeMoiTruongKCNByBaoCao(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _ketQuaBaoVeMoiTruongKCNService.GetAllByIdBaoCaoBaoVeMoiTruong(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }
    }
}
