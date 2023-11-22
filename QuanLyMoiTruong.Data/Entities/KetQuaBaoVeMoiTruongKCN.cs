using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class KetQuaBaoVeMoiTruongKCN : BaseEntity
    {
        public int IdKetQuaBaoVeMoiTruongKCN { get; set; }
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public virtual BaoCaoBaoVeMoiTruong BaoCaoBaoVeMoiTruong { get; set; }
        public int IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string DiaChi { get; set; }
        public string DienTich { get; set; }
        public string TenChuDautu { get; set; }
        public int SoLuongCoSo { get; set; }
        public string TyLeLapDay { get; set; }
        public bool HeThongThuGomNuocMua { get; set; }
        public string TongLuongNuocThai { get; set; }
        public string CongSuatThietKeHTXLNT { get; set; }
        public string HeThongQuanTracNuocThai { get; set; }
        public string ChatThaiRanSinhHoat { get; set; }
        public string ChatThaiRanCongNghiep { get; set; }
        public string ChatThaiRanNguyHai { get; set; }
        public string CongTrinhPhongNgua { get; set; }
        public string TyLeCayXanh { get; set; }
    }
}
