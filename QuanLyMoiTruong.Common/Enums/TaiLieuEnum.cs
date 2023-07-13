using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Common.Enums
{
    public enum LoaiFileTaiLieuEnum
    {
        [Display(Name = "Giấy phép môi trường")]
        GiayPhepMoiTruong = 110,
        [Display(Name = "Đơn xin cấp phép")]
        DonXinCapPhep = 111,
        [Display(Name = "Báo cáo đề xuất cấp phép")]
        BaoCaoDeXuatCapPhep = 112, 
        [Display(Name = "Dự án đầu tư")]
        DuAnDauTu = 113
    }
    public enum NhomTaiLieuEnum
    {
        [Display(Name = "Dự án")]
        DuAn = 1,
        [Display(Name = "Giấy phép môi trường")]
        GiayPhepMoiTruong = 11,
        [Display(Name = "Báo cáo công tác bảo vệ môi trường hằng năm")]
        BaoCaoBaoVeMoiTruongHangNam = 12,
        [Display(Name = "Hồ sơ thanh kiểm tra công tác bảo vệ môi trường")]
        HoSoKiemTraXuPhat = 13,

        [Display(Name = "Khu công nghiệp")]
        KhuCongNghiep = 2,
        [Display(Name = "Báo cáo bảo vệ môi trường Khu công nghiệp")]
        BaoCaoBaoVeMoiTruongKCN = 21,
        [Display(Name = "Báo cáo quan trắc môi trường định kỳ")]
        BaoCaoQuanTracMoiTruongDinhKy = 22,
    }
}
