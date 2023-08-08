﻿using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.ViewModels
{
    public class BaoCaoQuanTracMoiTruongViewModel
    {
        public int IdBaoCaoQuanTracMoiTruong { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string TenBaoCao { get; set; }
        public string NgayBaoCao { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();

    }
}
