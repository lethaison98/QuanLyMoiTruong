﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class ThanhPhanMoiTruongViewModel
    {
        public int IdThanhPhanMoiTruong { get; set; }
        public string TenThanhPhanMoiTruong { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; } = new List<FileTaiLieu>();

    }
}
