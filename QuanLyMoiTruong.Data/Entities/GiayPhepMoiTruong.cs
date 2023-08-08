using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class GiayPhepMoiTruong : BaseEntity
    {
        public int IdGiayPhepMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public virtual DuAn DuAn { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public virtual KhuCongNghiep KhuCongNghiep { get; set; }
        public string SoGiayPhep { get; set; }
        public string TenGiayPhep { get; set; }
        public string CoQuanCap { get; set; }
        public DateTime? NgayCap { get; set; }
    }
}
