using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoApiClient _baoCaoApiClient;
        private readonly IConfiguration _configuration;

        public BaoCaoController(IBaoCaoApiClient baoCaoApiClient,
            IConfiguration configuration)
        {
            _baoCaoApiClient = baoCaoApiClient;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BaoCaoCapGiayPhepMoiTruong()
        {
            var request = new GiayPhepMoiTruongRequest();
            var result = await _baoCaoApiClient.SearchBaoCaoCapGiayPhepMoiTruong(request);
            var model = result.Data;
            ViewBag.Data = model;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request, string command)
        {
            if (command.Equals("Search"))
            {
                var result = await _baoCaoApiClient.SearchBaoCaoCapGiayPhepMoiTruong(request);
                var model = result.Data;
                ViewBag.Data = model;
            }
            else
            {
                var data = await _baoCaoApiClient.ExportBaoCaoCapGiayPhepMoiTruong(request);
                if (data.Success)
                {
                    var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo cấp giấy phép môi trường.xlsx");
                    return result;
                }
                return Ok(data);
            }
            return View();
        }
    }
}
