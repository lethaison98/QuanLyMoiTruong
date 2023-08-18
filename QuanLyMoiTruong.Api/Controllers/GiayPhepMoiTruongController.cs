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
    public class GiayPhepMoiTruongController : Controller
    {
        private readonly IGiayPhepMoiTruongService _GiayPhepMoiTruongService;
        public GiayPhepMoiTruongController(IGiayPhepMoiTruongService GiayPhepMoiTruongService)
        {
            _GiayPhepMoiTruongService = GiayPhepMoiTruongService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _GiayPhepMoiTruongService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(GiayPhepMoiTruongViewModel req)
        {
            if(req.IdGiayPhepMoiTruong == 0)
            {
                var result = await _GiayPhepMoiTruongService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _GiayPhepMoiTruongService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new GiayPhepMoiTruongRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _GiayPhepMoiTruongService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idGiayPhepMoiTruong)
        {
            var result = await _GiayPhepMoiTruongService.Delete(idGiayPhepMoiTruong);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idGiayPhepMoiTruong)
        {
            var result = await _GiayPhepMoiTruongService.GetById(idGiayPhepMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetGPMTByDuAn")]
        public async Task<IActionResult> GetGPMTByDuAn(int idDuAn)
        {
            var result = await _GiayPhepMoiTruongService.GetListGiayPhepMoiTruongByDuAn(idDuAn);
            return Ok(result);
        }
        [HttpGet("GetGPMTByKhuCongNghiep")]
        public async Task<IActionResult> GetGPMTByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = await _GiayPhepMoiTruongService.GetListGiayPhepMoiTruongByKhuCongNghiep(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
