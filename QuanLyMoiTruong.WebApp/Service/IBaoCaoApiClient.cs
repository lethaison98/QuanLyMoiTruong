using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;

namespace QuanLyMoiTruong.WebApp.Service
{
    public interface IBaoCaoApiClient
    {
        Task<ApiResult<List<BaoCaoCapGiayPhepMoiTruongViewModel>>> SearchBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request);
        Task<ApiResult<byte[]>> ExportBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request);

        Task<ApiResult<List<KetQuaBaoVeMoiTruongKCNViewModel>>> SearchBaoCaoChiTieuBaoVeMoiTruongKCN(BaoCaoBaoVeMoiTruongRequest request);
        Task<ApiResult<byte[]>> ExportBaoCaoChiTieuBaoVeMoiTruongKCN(BaoCaoBaoVeMoiTruongRequest request);

        Task<ApiResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> SearchBaoCaoCacDoanhNghiepHoatDongTrongKCN(BaoCaoBaoVeMoiTruongRequest request);
        Task<ApiResult<byte[]>> ExportBaoCaoCacDoanhNghiepHoatDongTrongKCN(BaoCaoBaoVeMoiTruongRequest request);
    }
}
