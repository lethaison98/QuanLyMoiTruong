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
    public class KhuCongNghiepController : Controller
    {
        private readonly IKhuCongNghiepService _khuCongNghiepService;
        public KhuCongNghiepController(IKhuCongNghiepService khuCongNghiepService)
        {
            _khuCongNghiepService = khuCongNghiepService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _khuCongNghiepService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(KhuCongNghiep req)
        {
            if(req.IdKhuCongNghiep == 0)
            {
                var result = await _khuCongNghiepService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _khuCongNghiepService.Update(req);
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
            var result = await _khuCongNghiepService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idKhuCongNghiep)
        {
            var result = await _khuCongNghiepService.Delete(idKhuCongNghiep);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idKhuCongNghiep)
        {
            var result = await _khuCongNghiepService.GetById(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
