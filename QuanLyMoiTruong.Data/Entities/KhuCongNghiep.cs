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
        public string TenChuDauTu { get; set; }
        public string DiaDiem { get; set; }
        public bool ThuocKhuKinhTe { get; set; }
        public string GhiChu { get; set; }
        public List<DuAn> DsDuAn { get; set; }
        public List<BaoCaoBaoVeMoiTruong> DsBaoCaoBaoVeMoiTruong { get; set; }
        public List<GiayPhepMoiTruong> DsGiayPhepMoiTruong { get; set; }
        public List<HoSoKiemTraXuPhat> DsHoSoKiemTraXuPhat { get; set; }
        public List<BaoCaoQuanTracMoiTruong> DsBaoCaoQuanTracMoiTruong { get; set; }


    }
}
