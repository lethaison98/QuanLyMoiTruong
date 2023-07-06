using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Api.Models;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.Interfaces;

namespace QuanLyMoiTruong.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViecLamService _baiVietService;
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, IViecLamService baiVietService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _baiVietService = baiVietService;
        }

        public IActionResult Index()
        {
            var x =  _baiVietService.GetAll();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}