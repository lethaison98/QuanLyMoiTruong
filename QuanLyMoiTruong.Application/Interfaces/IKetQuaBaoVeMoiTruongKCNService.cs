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
    public interface IKetQuaBaoVeMoiTruongKCNService:IBaseService<KetQuaBaoVeMoiTruongKCN, int, KetQuaBaoVeMoiTruongKCNViewModel, PagingRequest>
    {
        public Task<ApiResult<bool>> InsertFromExcel(List<KetQuaBaoVeMoiTruongKCNViewModel> list);
        public Task<ApiResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>> GetAllByIdBaoCaoBaoVeMoiTruong(int idBaoCaoBaoVeMoiTruongKCN);
        public Task<ApiResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>> GetBaoCao1_2(BaoCaoBaoVeMoiTruongRequest request);

    }
}
