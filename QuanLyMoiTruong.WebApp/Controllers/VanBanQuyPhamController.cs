using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("VanBanQuyPham")]
    public class VanBanQuyPhamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("VanBanQuyPhamChiTiet/{id}")]
        public IActionResult VanBanQuyPhamChiTiet()
        {
            return View();
        }

        [Route("PopupDetailVanBanQuyPham")]
        public ViewResult _PopupDetailVanBanQuyPham()
        {
            return View();
        }

        [Route("PopupThongBaoVanBanQuyPham")]
        public ViewResult _PopupThongBaoVanBanQuyPham()
        {
            return View();
        }

        [Route("PopupImportVanBanQuyPham")]
        public ViewResult _PopupImportVanBanQuyPham()
        {
            return View();
        }
    }
}
