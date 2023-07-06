using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Services;
using QuanLyMoiTruong.Application.ViewModels;

namespace QuanLyMoiTruong.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaPhuongController : Controller
    {
        private readonly IDiaPhuongService _diaPhuongService;
        public DiaPhuongController(IDiaPhuongService diaPhuongService)
        {
            _diaPhuongService = diaPhuongService;
        }
        [HttpGet("GetAllTinhThanh")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTinhThanh()
        {
            var result = await _diaPhuongService.GetAllTinhThanh();
            return Ok(result);
        }
        [HttpGet("GetAllQuanHuyen")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllQuanHuyen()
        {
            var result = await _diaPhuongService.GetAllQuanHuyen();
            return Ok(result);
        }
        [HttpGet("GetAllPhuongXa")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPhuongXa()
        {
            var result = await _diaPhuongService.GetAllPhuongXa();
            return Ok(result);
        }
        [HttpGet("GetDiaPhuongById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDiaPhuongById()
        {
            int idDiaPhuong = 140;
            var result = await _diaPhuongService.GetDiaPhuongById(idDiaPhuong);
            return Ok(result);
        }
    }
}
