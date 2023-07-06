using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IDiaPhuongService
    {
        public Task<ApiResult<DiaPhuongViewModel>> GetDiaPhuongById(int idDiaPhuong);
        public Task<ApiResult<List<DiaPhuongViewModel>>> GetDsDiaPhuongByIds(string idsDiaPhuong);
        public Task<ApiResult<List<TinhThanh>>> GetAllTinhThanh();
        public Task<ApiResult<List<QuanHuyen>>> GetAllQuanHuyen();
        public Task<ApiResult<List<PhuongXa>>> GetAllPhuongXa();
    }
}
