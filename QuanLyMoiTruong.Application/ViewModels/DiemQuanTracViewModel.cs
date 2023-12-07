using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class DiemQuanTracViewModel
    {
        public int IdDiemQuanTrac { get; set; }
        public string TenDiemQuanTrac { get; set; }
        public string DiaChi { get; set; }
        public string Loai { get; set; }
        public string KinhDo { get; set; }
        public string ViDo { get; set; }
        public int? IdDuAn { get;set; }
        public int? IdKhuCongNghiep { get; set; }   
        public bool KhuKinhTe { get; set; } 
        public List<KetQuaQuanTrac> DsKetQuaQuanTrac { get; set; }

    }
}
