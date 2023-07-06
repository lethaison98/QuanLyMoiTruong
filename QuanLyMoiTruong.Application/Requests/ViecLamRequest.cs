using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class ViecLamRequest: PagingRequest
    {
        public string IdsDiaPhuong { get; set; } 
    }
}
