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
    public interface IKetQuaQuanTracService:IBaseService<KetQuaQuanTrac, int, KetQuaQuanTracViewModel, PagingRequest>
    {
        public Task<ApiResult<bool>> InsertFromExcel(List<KetQuaQuanTracViewModel> list);
        public Task<ApiResult<IList<KetQuaQuanTracViewModel>>> GetAllByIdThanhPhanMoiTruong(int idThanhPhanMoiTruong);

    }
}
