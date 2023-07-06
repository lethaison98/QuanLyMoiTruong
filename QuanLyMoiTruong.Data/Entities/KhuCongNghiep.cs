using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class KhuCongNghiep : BaseEntity
    {
        public int IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string GhiChu { get; set; }
        public List<BaoCaoBaoVeMoiTruongKCN> DsBaoCaoBaoVeMoiTruongKCN { get; set; }
        public List<BaoCaoQuanTracMoiTruongKCN> DsBaoCaoQuanTracMoiTruongKCN { get; set; }

    }
}
