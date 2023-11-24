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
    public interface IKetQuaBaoVeMoiTruongDoanhNghiepService:IBaseService<KetQuaBaoVeMoiTruongDoanhNghiep, int, KetQuaBaoVeMoiTruongDoanhNghiepViewModel, PagingRequest>
    {
        public Task<ApiResult<bool>> InsertFromExcel(List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel> list);
        public Task<ApiResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetAllByIdBaoCaoBaoVeMoiTruong(int idBaoCaoBaoVeMoiTruongDoanhNghiep);
        public Task<ApiResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetBaoCao2(BaoCaoBaoVeMoiTruongRequest request);

    }
}
