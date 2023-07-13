using Microsoft.AspNetCore.Http;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Request
{
    public class FileTaiLieuRequest
    {
        public string NhomTaiLieu { get; set; }
        public int IdTaiLieu { get; set; }
        public List<FileTaiLieu> FileTaiLieu { get; set; }

    }
}
