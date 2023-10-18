using QuanLyMoiTruong.Application.Requests;
using QuanLyMoiTruong.Application.ViewModels;

namespace QuanLyMoiTruong.WebApp.Service
{
    public interface IBaoCaoApiClient
    {
        Task<ApiResult<List<BaoCaoCapGiayPhepMoiTruongViewModel>>> SearchBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request);
        Task<ApiResult<byte[]>> ExportBaoCaoCapGiayPhepMoiTruong(GiayPhepMoiTruongRequest request);
    }
}
