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
        public int IdDuAn { get; set; }
        public string TenDuAn { get; set; }  
        public string TenDoanhNghiep { get; set; }  
        public string SoGiayPhep { get; set; }
        public string TenGiayPhep { get; set; }
        public string NgayCap { get; set; }
    }
}
