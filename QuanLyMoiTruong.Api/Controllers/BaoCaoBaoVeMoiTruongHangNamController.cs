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
    public class BaoCaoBaoVeMoiTruongHangNamController : Controller
    {
        private readonly IBaoCaoBaoVeMoiTruongHangNamService _baoCaoBaoVeMoiTruongHangNamService;
        public BaoCaoBaoVeMoiTruongHangNamController(IBaoCaoBaoVeMoiTruongHangNamService baoCaoBaoVeMoiTruongHangNamService)
        {
            _baoCaoBaoVeMoiTruongHangNamService = baoCaoBaoVeMoiTruongHangNamService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _baoCaoBaoVeMoiTruongHangNamService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoBaoVeMoiTruongHangNamViewModel req)
        {
            if(req.IdBaoCaoBaoVeMoiTruongHangNam == 0)
            {
                var result = await _baoCaoBaoVeMoiTruongHangNamService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _baoCaoBaoVeMoiTruongHangNamService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoBaoVeMoiTruongHangNamRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _baoCaoBaoVeMoiTruongHangNamService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoBaoVeMoiTruongHangNam)
        {
            var result = await _baoCaoBaoVeMoiTruongHangNamService.Delete(idBaoCaoBaoVeMoiTruongHangNam);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoBaoVeMoiTruongHangNam)
        {
            var result = await _baoCaoBaoVeMoiTruongHangNamService.GetById(idBaoCaoBaoVeMoiTruongHangNam);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTHangNamByDuAn")]
        public async Task<IActionResult> GetGPMTByDuAn(int idDuAn)
        {
            var result = await _baoCaoBaoVeMoiTruongHangNamService.GetListBaoCaoBaoVeMoiTruongHangNamByDuAn(idDuAn);
            return Ok(result);
        }
    }
}
