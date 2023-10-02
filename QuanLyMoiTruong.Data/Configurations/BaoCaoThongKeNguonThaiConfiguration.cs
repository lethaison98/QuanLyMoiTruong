using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoThongKeNguonThaiConfiguration : IEntityTypeConfiguration<BaoCaoThongKeNguonThai>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoThongKeNguonThai> builder)
        {
            builder.ToTable("BaoCaoThongKeNguonThai");
            builder.HasKey(x => x.IdBaoCaoThongKeNguonThai);
            builder.HasOne(x => x.DuAn).WithMany(hd => hd.DsBaoCaoThongKeNguonThai).HasForeignKey(x => x.IdDuAn);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsBaoCaoThongKeNguonThai).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
