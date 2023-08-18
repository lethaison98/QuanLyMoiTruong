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
    public class HoSoKiemTraXuPhatController : Controller
    {
        private readonly IHoSoKiemTraXuPhatService _hoSoKiemTraXuPhatService;
        public HoSoKiemTraXuPhatController(IHoSoKiemTraXuPhatService hoSoKiemTraXuPhatService)
        {
            _hoSoKiemTraXuPhatService = hoSoKiemTraXuPhatService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _hoSoKiemTraXuPhatService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(HoSoKiemTraXuPhatViewModel req)
        {
            if(req.IdHoSoKiemTraXuPhat == 0)
            {
                var result = await _hoSoKiemTraXuPhatService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _hoSoKiemTraXuPhatService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new HoSoKiemTraXuPhatRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _hoSoKiemTraXuPhatService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idhoSoKiemTraXuPhat)
        {
            var result = await _hoSoKiemTraXuPhatService.Delete(idhoSoKiemTraXuPhat);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idhoSoKiemTraXuPhat)
        {
            var result = await _hoSoKiemTraXuPhatService.GetById(idhoSoKiemTraXuPhat);
            return Ok(result);
        }
        [HttpGet("GetHoSoKiemTraXuPhatByDuAn")]
        public async Task<IActionResult> GetGPMTByDuAn(int idDuAn)
        {
            var result = await _hoSoKiemTraXuPhatService.GetListHoSoKiemTraXuPhatByDuAn(idDuAn);
            return Ok(result);
        }
        [HttpGet("GetHoSoKiemTraXuPhatByKhuCongNghiep")]
        public async Task<IActionResult> GetGPMTByKhuCongNghiep(int idKhuCongNghiep)
        {
            var result = await _hoSoKiemTraXuPhatService.GetListHoSoKiemTraXuPhatByKhuCongNghiep(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
