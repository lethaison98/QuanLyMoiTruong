using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class BaoCaoQuanTracMoiTruongRequest : PagingRequest
    {
        public int IdKhuCongNghiep { get; set; } 
    }
}
