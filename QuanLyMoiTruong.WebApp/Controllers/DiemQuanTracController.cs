using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.WebApp.Service;

namespace QuanLyMoiTruong.WebApp.Controllers
{
    [Route("DiemQuanTrac")]
    public class DiemQuanTracController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("DiemQuanTracChiTiet/{id}")]
        public IActionResult DiemQuanTracChiTiet()
        {
            return View();
        }

        [Route("PopupDetailDiemQuanTrac")]
        public ViewResult _PopupDetailDiemQuanTrac()
        {
            return View();
        }

        [Route("PopupImportDiemQuanTrac")]
        public ViewResult _PopupImportDiemQuanTrac()
        {
            return View();
        }
    }
}
