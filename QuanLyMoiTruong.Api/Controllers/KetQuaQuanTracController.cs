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
    public class KetQuaQuanTracController : Controller
    {
        private readonly IKetQuaQuanTracService _KetQuaQuanTracService;
        public KetQuaQuanTracController(IKetQuaQuanTracService KetQuaQuanTracService)
        {
            _KetQuaQuanTracService = KetQuaQuanTracService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _KetQuaQuanTracService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(KetQuaQuanTracViewModel req)
        {
            if(req.IdKetQuaQuanTrac == 0)
            {
                var result = await _KetQuaQuanTracService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _KetQuaQuanTracService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new PagingRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _KetQuaQuanTracService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idKetQuaQuanTrac)
        {
            var result = await _KetQuaQuanTracService.Delete(idKetQuaQuanTrac);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idKetQuaQuanTrac)
        {
            var result = await _KetQuaQuanTracService.GetById(idKetQuaQuanTrac);
            return Ok(result);
        }
        [HttpGet("GetAllByIdThanhPhanMoiTruong")]
        public async Task<IActionResult> GetAllByIdThanhPhanMoiTruong(int idThanhPhanMoiTruong)
        {
            var result = await _KetQuaQuanTracService.GetAllByIdThanhPhanMoiTruong(idThanhPhanMoiTruong);
            return Ok(result);
        }
    }
}
