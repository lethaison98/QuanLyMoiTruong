using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class GiayPhepMoiTruongRequest : PagingRequest
    {
        public int IdDuAn { get; set; } 
        public int IdKhuCongNghiep { get; set; }
        public string TenGiayPhep { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
