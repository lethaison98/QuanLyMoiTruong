using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class KetQuaQuanTracConfiguration : IEntityTypeConfiguration<KetQuaQuanTrac>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KetQuaQuanTrac> builder)
        {
            builder.ToTable("KetQuaQuanTrac");
            builder.HasKey(x => x.IdKetQuaQuanTrac);
            builder.HasOne(x => x.ThanhPhanMoiTruong).WithMany(hd => hd.DsKetQuaQuanTrac).HasForeignKey(x => x.IdThanhPhanMoiTruong);
            builder.HasOne(x => x.DiemQuanTrac).WithMany(hd => hd.DsKetQuaQuanTrac).HasForeignKey(x => x.IdDiemQuanTrac);
        }
    }
}
