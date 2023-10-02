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
    public interface IGiayPhepMoiTruongService:IBaseService<GiayPhepMoiTruong, int, GiayPhepMoiTruongViewModel, GiayPhepMoiTruongRequest>
    {
        public Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetListGiayPhepMoiTruongByDuAn(int idDuAn);
        public Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetListGiayPhepMoiTruongByKhuCongNghiep(int idKhuCongNghiep);
        public Task<ApiResult<IList<GiayPhepMoiTruongViewModel>>> GetListByKhoangThoiGian(GiayPhepMoiTruongRequest request);
    }
}
