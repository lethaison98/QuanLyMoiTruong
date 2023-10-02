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
    public class BaoCaoThongKeNguonThaiController : Controller
    {
        private readonly IBaoCaoThongKeNguonThaiService _BaoCaoThongKeNguonThaiService;
        public BaoCaoThongKeNguonThaiController(IBaoCaoThongKeNguonThaiService BaoCaoThongKeNguonThaiService)
        {
            _BaoCaoThongKeNguonThaiService = BaoCaoThongKeNguonThaiService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BaoCaoThongKeNguonThaiService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoThongKeNguonThaiViewModel req)
        {
            if(req.IdBaoCaoThongKeNguonThai == 0)
            {
                var result = await _BaoCaoThongKeNguonThaiService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _BaoCaoThongKeNguonThaiService.Update(req);
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
            var result = await _BaoCaoThongKeNguonThaiService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoThongKeNguonThai)
        {
            var result = await _BaoCaoThongKeNguonThaiService.Delete(idBaoCaoThongKeNguonThai);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoThongKeNguonThai)
        {
            var result = await _BaoCaoThongKeNguonThaiService.GetById(idBaoCaoThongKeNguonThai);
            return Ok(result);
        }
    }
}
