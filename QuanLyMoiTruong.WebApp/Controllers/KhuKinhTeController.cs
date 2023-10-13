using Microsoft.AspNetCore.Mvc;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    public class KhuKinhTeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BaoCaoBaoVeMoiTruongKhuKinhTe()
        {
            return View();
        }
    }
}
