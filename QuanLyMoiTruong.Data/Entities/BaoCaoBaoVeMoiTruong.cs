using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Entities
{
    public class BaoCaoBaoVeMoiTruong : BaseEntity
    {
        public int IdBaoCaoBaoVeMoiTruong { get; set; }
        public int? IdDuAn { get; set; }
        public virtual DuAn DuAn { get; set; }
        public int? IdKhuCongNghiep { get; set; }
        public virtual KhuCongNghiep KhuCongNghiep { get; set; }   
        public string LoaiBaoCao { get; set; }
        public bool KhuKinhTe { get; set; }
        public string TenBaoCao { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        public int Nam { get; set; }
        public int Lan { get; set; }
        [JsonIgnore]
        public List<KetQuaBaoVeMoiTruongDoanhNghiep> DsKetQuaBaoVeMoiTruongDoanhNghiep { get; set; }
        
        [JsonIgnore]
        public List<KetQuaBaoVeMoiTruongKCN> DsKetQuaBaoVeMoiTruongKCN{ get; set; }

    }
}
