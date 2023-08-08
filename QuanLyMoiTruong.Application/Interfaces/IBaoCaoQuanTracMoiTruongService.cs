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
    public interface IBaoCaoQuanTracMoiTruongService:IBaseService<BaoCaoQuanTracMoiTruong, int, BaoCaoQuanTracMoiTruongViewModel, BaoCaoQuanTracMoiTruongRequest>
    {
        public Task<ApiResult<IList<BaoCaoQuanTracMoiTruongViewModel>>> GetListBaoCaoQuanTracMoiTruongByKCN(int idDuAn);

    }
}
