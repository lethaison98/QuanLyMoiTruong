using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class BaoCaoBaoVeMoiTruongViewModel
    {
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string TenDoanhNghiep { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string TenChuDauTu { get; set; }
        public string TenBaoCao { get; set; }
        public string NgayBaoCao { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();

    }
}
