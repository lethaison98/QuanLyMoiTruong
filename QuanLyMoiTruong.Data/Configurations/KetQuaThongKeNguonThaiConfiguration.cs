using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class KetQuaThongKeNguonThaiConfiguration : IEntityTypeConfiguration<KetQuaThongKeNguonThai>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KetQuaThongKeNguonThai> builder)
        {
            builder.ToTable("KetQuaThongKeNguonThai");
            builder.HasKey(x => x.IdKetQuaThongKeNguonThai);
            builder.HasOne(x => x.BaoCaoThongKeNguonThai).WithMany(hd => hd.DsKetQuaThongKeNguonThai).HasForeignKey(x => x.IdBaoCaoThongKeNguonThai);
        }
    }
}
