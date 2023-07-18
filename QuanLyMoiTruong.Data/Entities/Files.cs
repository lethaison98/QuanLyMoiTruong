using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class Files : BaseEntity
    {
        public int IdFile { get; set; }
        public string LinkFile { get; set; }
        public string TenFile { get; set; }
        [JsonIgnore]
        public FileTaiLieu FileTaiLieu { get; set; }
    }
}
