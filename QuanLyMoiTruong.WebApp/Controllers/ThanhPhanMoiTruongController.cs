using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("ThanhPhanMoiTruong")]
    public class ThanhPhanMoiTruongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("ThanhPhanMoiTruongChiTiet/{id}")]
        public IActionResult ThanhPhanMoiTruongChiTiet()
        {
            return View();
        }

        [Route("PopupDetailThanhPhanMoiTruong")]
        public ViewResult _PopupDetailThanhPhanMoiTruong()
        {
            return View();
        }

        [Route("PopupThongBaoThanhPhanMoiTruong")]
        public ViewResult _PopupThongBaoThanhPhanMoiTruong()
        {
            return View();
        }

        [Route("PopupImportThanhPhanMoiTruong")]
        public ViewResult _PopupImportThanhPhanMoiTruong()
        {
            return View();
        }
    }
}
