using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IDiemQuanTracService:IBaseService<DiemQuanTrac, int, DiemQuanTracViewModel, PagingRequest>
    {
        public Task<ApiResult<IList<DiemQuanTracViewModel>>> GetDuLieuLenBanDo(int idThanhPhanMoiTruong);
    }
}
