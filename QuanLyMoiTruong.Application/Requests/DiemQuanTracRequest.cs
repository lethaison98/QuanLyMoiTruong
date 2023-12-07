using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class DiemQuanTracRequest: PagingRequest
    {
        public string Type { get; set; } 
    }
}
