﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class KetQuaQuanTrac : BaseEntity
    {
        public int IdKetQuaQuanTrac { get; set; }
        public int IdDiemQuanTrac { get; set; }
        public virtual DiemQuanTrac DiemQuanTrac { get; set; }
        public int IdThanhPhanMoiTruong { get; set; }
        public virtual ThanhPhanMoiTruong ThanhPhanMoiTruong { get; set; }  
        public int Nam { get; set; }
        public int Lan { get; set; }
        public string ChiTieu { get; set; }
        public string GiaTri { get; set; }
        public string DonViTinh { get; set; }
        public string TieuChuan { get; set; }
        public string NguongToiThieu { get; set; }
        public string NguongToiDa { get; set; }
    }
}
