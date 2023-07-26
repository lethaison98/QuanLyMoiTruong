using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using System.Security.Claims;

namespace QuanLyMoiTruong.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("InsertUpdate")]
        public async Task<IActionResult> InsertUpdate(UserViewModel request)
        {
            if (request.IdUser == new Guid())
            {
                var result = await _userService.Insert(request);
                return Ok(result);
            }
            else
            {
                var result = await _userService.Update(request);
                return Ok(result);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging(string keyword = "", int pageNumber = 1, int pageSize = 10)
        {
            var param = new PagingRequest
            {
                FullTextSearch = keyword,
                PageIndex = pageNumber,
                PageSize = pageSize
            };
            var result = await _userService.GetAllPaging(param);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid idUser)
        {
            var result = await _userService.Delete(idUser);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid idUser)
        {
            var result = await _userService.GetById(idUser);
            return Ok(result);
        }

    }
}


