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
    public class BaoCaoBaoVeMoiTruongKCNController : Controller
    {
        private readonly IBaoCaoBaoVeMoiTruongKCNService _baoCaoBaoVeMoiTruongKCNService;
        public BaoCaoBaoVeMoiTruongKCNController(IBaoCaoBaoVeMoiTruongKCNService baoCaoBaoVeMoiTruongKCNService)
        {
            _baoCaoBaoVeMoiTruongKCNService = baoCaoBaoVeMoiTruongKCNService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _baoCaoBaoVeMoiTruongKCNService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoBaoVeMoiTruongKCNViewModel req)
        {
            if(req.IdBaoCaoBaoVeMoiTruongKCN == 0)
            {
                var result = await _baoCaoBaoVeMoiTruongKCNService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _baoCaoBaoVeMoiTruongKCNService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoBaoVeMoiTruongKCNRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _baoCaoBaoVeMoiTruongKCNService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoBaoVeMoiTruongKCN)
        {
            var result = await _baoCaoBaoVeMoiTruongKCNService.Delete(idBaoCaoBaoVeMoiTruongKCN);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoBaoVeMoiTruongKCN)
        {
            var result = await _baoCaoBaoVeMoiTruongKCNService.GetById(idBaoCaoBaoVeMoiTruongKCN);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoBVMTByKCN")]
        public async Task<IActionResult> GetGPMTByDuAn(int idKhuCongNghiep)
        {
            var result = await _baoCaoBaoVeMoiTruongKCNService.GetListBaoCaoBaoVeMoiTruongByKCN(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
