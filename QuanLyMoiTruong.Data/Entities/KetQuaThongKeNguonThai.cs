using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class KetQuaThongKeNguonThai : BaseEntity
    {
        public int IdKetQuaThongKeNguonThai { get; set; }
        public int IdBaoCaoThongKeNguonThai { get; set; }
        public virtual BaoCaoThongKeNguonThai BaoCaoThongKeNguonThai { get; set; }
        public int IdKhuCongNghiep { get; set; }
        public string TenKhuCongNghiep { get; set; }
        public string NuocThaiSinhHoat { get; set; }
        public string NuocThaiSanXuat { get; set; }
        public string NuocThaiTaiSuDung { get; set; }
        public string LuuLuongDauNoi { get; set; }
        public string KhiThai { get; set; }
        public string ChatThaiRanSinhHoat { get; set; }
        public string ChatThaiRanSanXuat { get; set; }
        public string ChatThaiRanTaiSuDung { get; set; }
        public string TongChatThaiRan { get; set; }
        public string ChatThaiNguyHai { get; set; }
    }
}
