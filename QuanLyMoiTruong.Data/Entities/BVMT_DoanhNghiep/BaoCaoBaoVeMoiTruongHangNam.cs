using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class BaoCaoBaoVeMoiTruongHangNam : BaseEntity
    {
        public int IdBaoCaoBaoVeMoiTruongHangNam { get; set; }
        public int IdDuAn { get; set; }
        public virtual DuAn DuAn { get; set; }   
        public string TenBaoCao { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        
    }
}
