using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class TinhThanh
    {
        public int IdTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }
        public string Cap { get; set; }
        public List<QuanHuyen> DSQuanHuyen { get; set; }
    }
}
