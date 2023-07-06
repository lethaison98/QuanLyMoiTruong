using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class ViecLam:BaseEntity
    {
        public int IdViecLam { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }    
        public string YeuCau { get; set; }
        public string QuyenLoi { get;set; }
        public string DiaDiem { get; set; }
        [Precision(18, 2)]
        public decimal MucLuongToiThieu { get; set; }
        [Precision(18, 2)]
        public decimal MucLuongToiDa { get; set; }
        public string DonViTienTe { get; set; }
        public string DonViThoiGian { get; set; }
        public DateTime? TuyenDungTuNgay { get; set; }
        public DateTime? TuyenDungDenNgay { get; set; }
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
        public DateTime? NgayPheDuyet { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string  UserPheDuyet { get; set; }
    }
}
