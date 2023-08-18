using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("DuAn")]
    public class DuAnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("DuAnChiTiet/{id}")]
        public IActionResult DuAnChiTiet()
        {
            return View();
        }
    }
}
