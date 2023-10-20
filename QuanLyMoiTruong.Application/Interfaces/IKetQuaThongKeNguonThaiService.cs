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
    public interface IKetQuaThongKeNguonThaiService:IBaseService<KetQuaThongKeNguonThai, int, KetQuaThongKeNguonThaiViewModel, PagingRequest>
    {
        public Task<ApiResult<bool>> InsertFromExcel(List<KetQuaThongKeNguonThaiViewModel> list);
        public Task<ApiResult<List<KetQuaThongKeNguonThaiViewModel>>> GetAllByIdBaoCaoThongKeNguonThai(int idBaoCaoThongKeNguonThai);

    }
}
