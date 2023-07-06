using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class ViecLamViewModel
    {
        public int IdViecLam { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string YeuCau { get; set; }
        public string QuyenLoi { get; set; }
        public string DiaDiem { get; set; }
        public decimal MucLuongToiThieu { get; set; }
        public decimal MucLuongToiDa { get; set; }
        public string DonViTienTe { get; set; }
        public string DonViThoiGian { get; set; }
        public string TuyenDungTuNgay { get; set; }
        public string TuyenDungDenNgay { get; set; }
        public string ThongTinNhaTuyenDung { get; set; }
        public string ThongTinKhac { get; set; }
        public string DSIdDiaPhuong { get; set; }
        public string DSIdNganhNghe { get; set; }
        public string DSIdPhucLoi { get; set; }

        //Thông tin hệ thống
        public string Url { get; set; }
        public int TrangThai { get; set; }
        public int MucUuTien { get; set; }
        public int XepHang { get; set; }
        public string NgayPheDuyet { get; set; }
        public string NgayHetHan { get; set; }
        public string UserPheDuyet { get; set; }
    }
}
