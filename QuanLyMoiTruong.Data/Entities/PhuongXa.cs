using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class PhuongXa
    {
        public int IdPhuongXa { get; set; }
        public string TenPhuongXa { get; set; }
        public int IdQuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public int IdTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }
        public string Cap { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }

    }
}
