using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class KetQuaBaoVeMoiTruongDoanhNghiepViewModel
    {
        public int IdKetQuaBaoVeMoiTruongDoanhNghiep { get; set; }
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public int IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string SoGiayTo { get; set; }
        public string LoaiHinhSanXuat { get; set; }
        public string TongLuongNuocThai { get; set; }
        public string DauNoiVaoHTXLNT { get; set; }
        public string TachDauNoi { get; set; }
        public string LuongKhiThai { get; set; }
        public string QuanTracKhiThai { get; set; }
        public string ChatThaiRanSinhHoat { get; set; }
        public string ChatThaiRanCongNghiep { get; set; }
        public string ChatThaiRanNguyHai { get; set; }
        public string TyLeCayXanh { get; set; }
    }
}
