using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class DuAnViewModel
    {
        public int IdDuAn { get; set; }
        public int? IdKhuCongNghiep { get; set; }
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
        public List<GiayPhepMoiTruongViewModel> DSGiayPhepMoiTruong { get; set; }    
    }
}
