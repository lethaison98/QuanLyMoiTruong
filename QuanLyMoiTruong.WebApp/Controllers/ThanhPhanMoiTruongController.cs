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

        [Route("BanDoCacThanhPhanMoitruong")]
        public ViewResult BanDoCacThanhPhanMoitruong()
        {
            return View();
        }
    }
}
