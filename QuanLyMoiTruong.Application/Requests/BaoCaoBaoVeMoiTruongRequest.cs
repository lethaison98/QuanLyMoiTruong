using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class BaoCaoBaoVeMoiTruongRequest : PagingRequest
    {
        public int IdDuAn { get; set; } 
    }
}
