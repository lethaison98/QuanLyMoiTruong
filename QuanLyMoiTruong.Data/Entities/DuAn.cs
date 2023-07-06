﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class DuAn : BaseEntity
    {
        public int IdDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string TenDoanhNghiep { get; set; }
        public string DiaChi { get; set; }
        public string TenNguoiDaiDien { get; set; }
        public string TenNguoiPhuTrachTNMT { get; set; }
        public string GiayPhepDKKD { get; set; }
        public string GhiChu { get; set; }
        public List<GiayPhepMoiTruong> DsGiayPhepMoiTruong { get; set; }
        public List<BaoCaoBaoVeMoiTruongHangNam> DsBaoCaoBaoVeMoiTruongHangNam { get; set; }
        public List<HoSoKiemTraXuPhat> DsHoSoKiemTraXuPhat { get; set; }
    }
}
