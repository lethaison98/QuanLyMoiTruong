using Microsoft.AspNetCore.Http;
using QuanLyMoiTruong.Application.Request;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IFileTaiLieuService
    {
        public Task<ApiResult<int>> Insert(int idFile, int idTaiLieu, string nhomTaiLieu, string loaiFileTaiLieu);
        public Task<ApiResult<int>> UpdateAll(FileTaiLieuRequest request);
        public Task<ApiResult<IList<FileTaiLieu>>> GetByTaiLieu(int idTaiLieu, string nhomTaiLieu);
        public Task<ApiResult<bool>> Delete(int idFileTaiLieu);
        //public Task<ApiResult<QuyetDinhMienTienThueDatViewModel>> GetById(int idQuyetDinhMienTienThueDat);
        //public Task<ApiResult<List<QuyetDinhMienTienThueDatViewModel>>> GetAll(int? idDoanhNghiep);
        //public Task<ApiResult<PageViewModel<QuyetDinhMienTienThueDatViewModel>>> GetAllPaging(int? idDoanhNghiep,string keyword, int pageIndex, int pageSize);
    }
}
