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
    public interface IBaoCaoThongKeNguonThaiService:IBaseService<BaoCaoThongKeNguonThai, int, BaoCaoThongKeNguonThaiViewModel, PagingRequest>
    {
        public Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByDuAn(int idDuAn);
        public Task<ApiResult<IList<BaoCaoThongKeNguonThaiViewModel>>> GetListBaoCaoThongKeNguonThaiByKhuCongNghiep(int idKhuCongNghiep);
        public Task<ApiResult<List<KetQuaThongKeNguonThaiViewModel>>> GetKetQuaThongKeNguonThaiByIdBaoCao(int id);
    }
}
