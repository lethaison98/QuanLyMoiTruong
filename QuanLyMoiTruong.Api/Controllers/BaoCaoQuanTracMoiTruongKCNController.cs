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
    public class BaoCaoQuanTracMoiTruongKCNController : Controller
    {
        private readonly IBaoCaoQuanTracMoiTruongKCNService _baoCaoQuanTracMoiTruongKCNService;
        public BaoCaoQuanTracMoiTruongKCNController(IBaoCaoQuanTracMoiTruongKCNService baoCaoQuanTracMoiTruongKCNService)
        {
            _baoCaoQuanTracMoiTruongKCNService = baoCaoQuanTracMoiTruongKCNService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _baoCaoQuanTracMoiTruongKCNService.GetAll();
            return Ok(result);
        }
        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(BaoCaoQuanTracMoiTruongKCNViewModel req)
        {
            if(req.IdBaoCaoQuanTracMoiTruongKCN == 0)
            {
                var result = await _baoCaoQuanTracMoiTruongKCNService.Insert(req);
                return Ok(result);
            }
            else
            {
                var result = await _baoCaoQuanTracMoiTruongKCNService.Update(req);
                return Ok(result);
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new BaoCaoQuanTracMoiTruongKCNRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _baoCaoQuanTracMoiTruongKCNService.GetAllPaging(param);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int idBaoCaoQuanTracMoiTruongKCN)
        {
            var result = await _baoCaoQuanTracMoiTruongKCNService.Delete(idBaoCaoQuanTracMoiTruongKCN);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int idBaoCaoQuanTracMoiTruongKCN)
        {
            var result = await _baoCaoQuanTracMoiTruongKCNService.GetById(idBaoCaoQuanTracMoiTruongKCN);
            return Ok(result);
        }
        [HttpGet("GetBaoCaoQTMTByKCN")]
        public async Task<IActionResult> GetGPMTByDuAn(int idKhuCongNghiep)
        {
            var result = await _baoCaoQuanTracMoiTruongKCNService.GetListBaoCaoQuanTracMoiTruongByKCN(idKhuCongNghiep);
            return Ok(result);
        }
    }
}
