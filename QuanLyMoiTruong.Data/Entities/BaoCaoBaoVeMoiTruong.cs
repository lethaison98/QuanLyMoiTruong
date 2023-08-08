using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class BaoCaoBaoVeMoiTruong : BaseEntity
    {
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public virtual DuAn DuAn { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public virtual KhuCongNghiep KhuCongNghiep { get; set; }   
        public string TenBaoCao { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        
    }
}
