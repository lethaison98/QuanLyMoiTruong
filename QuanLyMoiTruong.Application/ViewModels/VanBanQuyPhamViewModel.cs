using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class VanBanQuyPhamViewModel
    {
        public int IdVanBanQuyPham { get; set; }
        public string SoKyHieu { get; set; }
        public string TrichYeu { get; set; }
        public string NgayBanHanh { get; set; }
        public string CoQuanBanHanh { get; set; }
        public int Nam { get; set; }
        public string GhiChu { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();

    }
}
