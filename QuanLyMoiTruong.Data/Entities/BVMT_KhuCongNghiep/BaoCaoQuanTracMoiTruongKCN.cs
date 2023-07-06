using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class BaoCaoQuanTracMoiTruongKCN : BaseEntity
    {
        public int IdBaoCaoQuanTracMoiTruongKCN { get; set; }
        public int IdKhuCongNghiep { get; set; }
        public virtual KhuCongNghiep KhuCongNghiep { get; set; }
        public string TenBaoCao { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        
    }
}
