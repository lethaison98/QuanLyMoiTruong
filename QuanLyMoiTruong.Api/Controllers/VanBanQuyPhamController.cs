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
    public class VanBanQuyPhamController : Controller
    {
        private readonly IVanBanQuyPhamService _VanBanQuyPhamService;
        public VanBanQuyPhamController(IVanBanQuyPhamService VanBanQuyPhamService)
        {
            _VanBanQuyPhamService = VanBanQuyPhamService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _VanBanQuyPhamService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(VanBanQuyPhamViewModel req)
        {
            if(req.IdVanBanQuyPham == 0)
            {
                var result = await _VanBanQuyPhamService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _VanBanQuyPhamService.Update(req);
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
            var result = await _VanBanQuyPhamService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idVanBanQuyPham)
        {
            var result = await _VanBanQuyPhamService.Delete(idVanBanQuyPham);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idVanBanQuyPham)
        {
            var result = await _VanBanQuyPhamService.GetById(idVanBanQuyPham);
            return Ok(result);
        }
    }
}
