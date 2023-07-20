using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class HoSoKiemTraXuPhatViewModel
    {
        public int IdHoSoKiemTraXuPhat { get; set; }
        public int IdDuAn { get; set; }
        public string TenDuAn { get; set; }  
        public string TenDoanhNghiep { get; set; }  
        public string TenHoSo { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();
    }
}
