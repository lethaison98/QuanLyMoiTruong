using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("KhuCongNghiep")]
    public class KhuCongNghiepController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("KhuCongNghiepChiTiet/{id}")]
        public IActionResult KhuCongNghiepChiTiet()
        {
            return View();
        }

        [Route("PopupDetailKhuCongNghiep")]
        public ViewResult _PopupDetailKhuCongNghiep()
        {
            return View();
        }

        [Route("PopupThongBaoKhuCongNghiep")]
        public ViewResult _PopupThongBaoKhuCongNghiep()
        {
            return View();
        }

        [Route("PopupImportKhuCongNghiep")]
        public ViewResult _PopupImportKhuCongNghiep()
        {
            return View();
        }
    }
}
