using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class ThanhPhanMoiTruong : BaseEntity
    {
        public int IdThanhPhanMoiTruong { get; set; }
        public string TenThanhPhanMoiTruong { get; set; }
        public int Nam { get; set; }
        public int Lan { get; set; }
        public string GhiChu { get; set; }
        [JsonIgnore]
        public List<KetQuaQuanTrac> DsKetQuaQuanTrac { get; set; }

    }
}
