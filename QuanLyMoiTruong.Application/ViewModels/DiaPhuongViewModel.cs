using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class DiaPhuongViewModel
    {
        public int IdDiaPhuong { get; set; }
        public string TenDiaPhuong { get; set; }
        public string TenDayDu { get; set; }
        public int IdDiaPhuongCha { get; set; }
        public string Cap { get; set; }
        public string Type { get; set; }
        public List<DiaPhuongViewModel> DiaPhuongCon { get; set; }

    }
}
