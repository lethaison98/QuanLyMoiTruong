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
    public class DuAnController : Controller
    {
        private readonly IDuAnService _duAnService;
        public DuAnController(IDuAnService duAnService)
        {
            _duAnService = duAnService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _duAnService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(DuAnViewModel req)
        {
            if(req.IdDuAn == 0)
            {
                var result = await _duAnService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _duAnService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new DuAnRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _duAnService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idDuAn)
        {
            var result = await _duAnService.Delete(idDuAn);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idDuAn)
        {
            var result = await _duAnService.GetById(idDuAn);
            return Ok(result);
        }
    }
}
