using Microsoft.AspNetCore.Mvc;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("BaoCaoThongKeNguonThai")]
    public class BaoCaoThongKeNguonThaiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("BaoCaoThongKeNguonThaiChiTiet/{id}")]
        public IActionResult BaoCaoThongKeNguonThaiChiTiet()
        {
            return View();
        }
    }
}
