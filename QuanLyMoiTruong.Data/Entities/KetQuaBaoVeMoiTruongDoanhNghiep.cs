using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class KetQuaBaoVeMoiTruongDoanhNghiep : BaseEntity
    {
        public int IdKetQuaBaoVeMoiTruongDoanhNghiep { get; set; }
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public virtual BaoCaoBaoVeMoiTruong BaoCaoBaoVeMoiTruong { get; set; }
        public int IdDoanhNghiep { get; set; }
        public string TenDoanhNghiep { get; set; }
        public int IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string SoGiayTo { get; set; }
        public string LoaiHinhSanXuat { get; set; }
        public string TongLuongNuocThai { get; set; }
        public bool DauNoiVaoHTXLNT { get; set; }
        public bool TachDauNoi { get; set; }
        public string LuongKhiThai { get; set; }
        public bool QuanTracKhiThai { get; set; }
        public string ChatThaiRanSinhHoat { get; set; }
        public string ChatThaiRanCongNghiep { get; set; }
        public string ChatThaiRanNguyHai { get; set; }
        public string TyLeCayXanh { get; set; }
    }
}
