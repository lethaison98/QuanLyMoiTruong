using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class HoSoKiemTraXuPhat : BaseEntity
    {
        public int IdHoSoKiemTraXuPhat { get; set; }
        public int IdDuAn { get; set; }
        public virtual DuAn DuAn { get; set; }   
        public string TenHoSo { get; set; }
        
    }
}
