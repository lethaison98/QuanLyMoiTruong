using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Request
{
    public class FileUploadRequest
    {
        public string NhomTaiLieu { get; set; }
        public int IdTaiLieu { get; set; }
        public List<IFormFile> File { get; set; }

    }
}
