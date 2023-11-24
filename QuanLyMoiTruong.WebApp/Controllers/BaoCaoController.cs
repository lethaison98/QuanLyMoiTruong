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
        [HttpGet]
        public async Task<IActionResult> BaoCaoChiTieuBaoVeMoiTruongKhuKinhTe()
        {
            var request = new BaoCaoBaoVeMoiTruongRequest();
            //var result = await _baoCaoApiClient.SearchBaoCaoChiTieuBaoVeMoiTruongKCN(request);
            //var model = result.Data;
            //ViewBag.Data = model;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BaoCaoChiTieuBaoVeMoiTruongKhuKinhTe(BaoCaoBaoVeMoiTruongRequest request, string command)
        {
            //if (command.Equals("Search"))
            //{
            //    var result = await _baoCaoApiClient.SearchBaoCaoChiTieuBaoVeMoiTruongKhuKinhTe(request);
            //    var model = result.Data;
            //    ViewBag.Data = model;
            //}
            //else
            //{
            //    var data = await _baoCaoApiClient.ExportBaoCaoChiTieuBaoVeMoiTruongKhuKinhTe(request);
            //    if (data.Success)
            //    {
            //        var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Chỉ tiêu báo cáo môi trường đối với các KCN.xlsx");
            //        return result;
            //    }
            //    return Ok(data);
            //}
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BaoCaoChiTieuBaoVeMoiTruongKCN()
        {
            var request = new BaoCaoBaoVeMoiTruongRequest();
            var result = await _baoCaoApiClient.SearchBaoCaoChiTieuBaoVeMoiTruongKCN(request);
            var model = result.Data;
            ViewBag.Data = model;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BaoCaoChiTieuBaoVeMoiTruongKCN(BaoCaoBaoVeMoiTruongRequest request, string command)
        {
            if (command.Equals("Search"))
            {
                var result = await _baoCaoApiClient.SearchBaoCaoChiTieuBaoVeMoiTruongKCN(request);
                var model = result.Data;
                ViewBag.Data = model;
            }
            else
            {
                var data = await _baoCaoApiClient.ExportBaoCaoChiTieuBaoVeMoiTruongKCN(request);
                if (data.Success)
                {
                    var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Chỉ tiêu báo cáo môi trường đối với các KCN.xlsx");
                    return result;
                }
                return Ok(data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BaoCaoCacDoanhNghiepHoatDongTrongKCN()
        {
            var request = new BaoCaoBaoVeMoiTruongRequest();
            var result = await _baoCaoApiClient.SearchBaoCaoCacDoanhNghiepHoatDongTrongKCN(request);
            var model = result.Data;
            ViewBag.Data = model;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BaoCaoCacDoanhNghiepHoatDongTrongKCN(BaoCaoBaoVeMoiTruongRequest request, string command)
        {
            if (command.Equals("Search"))
            {
                var result = await _baoCaoApiClient.SearchBaoCaoCacDoanhNghiepHoatDongTrongKCN(request);
                var model = result.Data;
                ViewBag.Data = model;
            }
            else
            {
                var data = await _baoCaoApiClient.ExportBaoCaoCacDoanhNghiepHoatDongTrongKCN(request);
                if (data.Success)
                {
                    var result = File(data.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh sách các cơ sở hoạt động trong các khu công nghiệp.xlsx");
                    return result;
                }
                return Ok(data);
            }
            return View();
        }


    }
}
