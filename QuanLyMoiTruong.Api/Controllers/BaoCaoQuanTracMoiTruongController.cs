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
    public class BaoCaoQuanTracMoiTruongController : Controller
    {
        private readonly IBaoCaoQuanTracMoiTruongService _BaoCaoQuanTracMoiTruongService;
        public BaoCaoQuanTracMoiTruongController(IBaoCaoQuanTracMoiTruongService BaoCaoQuanTracMoiTruongService)
        {
            _BaoCaoQuanTracMoiTruongService = BaoCaoQuanTracMoiTruongService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BaoCaoQuanTracMoiTruongService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoQuanTracMoiTruongViewModel req)
        {
            if(req.IdBaoCaoQuanTracMoiTruong == 0)
            {
                var result = await _BaoCaoQuanTracMoiTruongService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _BaoCaoQuanTracMoiTruongService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoQuanTracMoiTruongRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _BaoCaoQuanTracMoiTruongService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoQuanTracMoiTruong)
        {
            var result = await _BaoCaoQuanTracMoiTruongService.Delete(idBaoCaoQuanTracMoiTruong);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoQuanTracMoiTruong)
        {
            var result = await _BaoCaoQuanTracMoiTruongService.GetById(idBaoCaoQuanTracMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoQTMTByKCN")]
        public async Task<IActionResult> GetGPMTByDuAn(int idKhuCongNghiep)
        {
            var result = await _BaoCaoQuanTracMoiTruongService.GetListBaoCaoQuanTracMoiTruongByKCN(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
