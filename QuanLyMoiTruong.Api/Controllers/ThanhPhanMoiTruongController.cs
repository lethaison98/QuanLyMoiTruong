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
    public class ThanhPhanMoiTruongController : Controller
    {
        private readonly IThanhPhanMoiTruongService _thanhPhanMoiTruongService;
        public ThanhPhanMoiTruongController(IThanhPhanMoiTruongService thanhPhanMoiTruongService)
        {
            _thanhPhanMoiTruongService = thanhPhanMoiTruongService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _thanhPhanMoiTruongService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(ThanhPhanMoiTruongViewModel req)
        {
            if(req.IdThanhPhanMoiTruong == 0)
            {
                var result = await _thanhPhanMoiTruongService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _thanhPhanMoiTruongService.Update(req);
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
            var result = await _thanhPhanMoiTruongService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idThanhPhanMoiTruong)
        {
            var result = await _thanhPhanMoiTruongService.Delete(idThanhPhanMoiTruong);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idThanhPhanMoiTruong)
        {
            var result = await _thanhPhanMoiTruongService.GetById(idThanhPhanMoiTruong);
            return Ok(result);
        }
    }
}
