using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class GiayPhepMoiTruongViewModel
    {
        public int IdGiayPhepMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string TenDoanhNghiep { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string TenChuDauTu { get; set; }
        public string SoGiayPhep { get; set; }
        public string TenGiayPhep { get; set; }
        public string NgayCap { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();
    }
}
