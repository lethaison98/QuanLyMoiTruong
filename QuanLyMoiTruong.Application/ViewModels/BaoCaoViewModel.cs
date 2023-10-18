using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class BaoCaoCapGiayPhepMoiTruongViewModel
    {
        public int IdGiayPhepMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string TenDuAn { get; set; }
        public string TenDoanhNghiep { get; set; }
        public bool ThuocKhuKinhTe { get; set; }
        public string DiaChi { get; set; }
        public string TenNguoiDaiDien { get; set; }
        public string TenNguoiPhuTrachTNMT { get; set; }
        public string GiayPhepDKKD { get; set; }
        public string LoaiHinhSanXuat { get; set; }
        public string QuyMo { get; set; }
        public string GhiChu { get; set; }
        public string SoGiayPhep { get; set; }
        public string TenGiayPhep { get; set; }
        public string NgayCap { get; set; }
        public string CoQuanCap { get; set; }
    }
}
