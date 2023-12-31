﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class FileTaiLieu : BaseEntity
    {
        public int IdFileTaiLieu { get; set; }
        public int IdFile { get; set; }
        public int IdTaiLieu { get; set; }
        public string NhomTaiLieu { get; set; }
        public string LoaiFileTaiLieu { get; set; }
        public int TrangThai { get; set; }
        public string MoTa { get; set; }
        public Files File { get; set; }
    }
}
