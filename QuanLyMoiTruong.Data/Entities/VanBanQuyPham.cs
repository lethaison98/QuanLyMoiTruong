using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class VanBanQuyPham : BaseEntity
    {
        public int IdVanBanQuyPham { get; set; }
        public string SoKyHieu { get; set; }
        public string TrichYeu { get; set; }
        public DateTime? NgayBanHanh { get; set; }
        public string CoQuanBanHanh { get; set; }
        public int Nam { get; set; }
        public string GhiChu { get; set; }
    }
}
