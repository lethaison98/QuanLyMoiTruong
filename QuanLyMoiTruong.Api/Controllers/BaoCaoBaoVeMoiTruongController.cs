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
    public class BaoCaoBaoVeMoiTruongController : Controller
    {
        private readonly IBaoCaoBaoVeMoiTruongService _BaoCaoBaoVeMoiTruongService;
        public BaoCaoBaoVeMoiTruongController(IBaoCaoBaoVeMoiTruongService BaoCaoBaoVeMoiTruongService)
        {
            _BaoCaoBaoVeMoiTruongService = BaoCaoBaoVeMoiTruongService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoBaoVeMoiTruongViewModel req)
        {
            if(req.IdBaoCaoBaoVeMoiTruong == 0)
            {
                var result = await _BaoCaoBaoVeMoiTruongService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _BaoCaoBaoVeMoiTruongService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoBaoVeMoiTruongRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _BaoCaoBaoVeMoiTruongService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.Delete(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoBaoVeMoiTruong)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetById(idBaoCaoBaoVeMoiTruong);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTHangNamByDuAn")]
        public async Task<IActionResult> GetGPMTByDuAn(int idDuAn)
        {
            var result = await _BaoCaoBaoVeMoiTruongService.GetListBaoCaoBaoVeMoiTruongByDuAn(idDuAn);
            return Ok(result);
        }
    }
}
