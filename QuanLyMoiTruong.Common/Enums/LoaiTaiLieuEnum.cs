using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Common.Enums
{
    public enum LoaiTaiLieuEnum
    {
        [Display(Name = "Chưa nộp hồ sơ")]
        NotYet = 0,
        [Display(Name = "Đang xử lý")]
        Processing = 1, // Đang xử lý
        [Display(Name = "Chậm tiến độ")]
        SlowProgress = 2,//Chậm tiến độ
        [Display(Name = "Hoàn thành")]
        Finish = 3,//Kết thức dự án
        [Display(Name = "Lấy ý kiến")]
        HoldDear = 4
    }
    public enum NhomTaiLieuEnum
    {
        [Display(Name = "Giấy phép môi trường")]
        GiayPhepMoiTruong = 110,
        [Display(Name = "Báo cáo công tác bảo vệ môi trường hằng năm")]
        BaoCaoBaoVeMoiTruongHangNam = 120,
        [Display(Name = "Hồ sơ thanh kiểm tra công tác bảo vệ môi trường")]
        HoSoKiemTraXuPhat = 130,

        [Display(Name = "Báo cáo bảo vệ môi trường Khu công nghiệp")]
        BaoCaoBaoVeMoiTruongKCN = 210,
        [Display(Name = "Báo cáo quan trắc môi trường định kỳ")]
        BaoCaoQuanTracMoiTruongDinhKy = 220,
    }
}
