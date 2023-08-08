using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoQuanTracMoiTruongConfiguration : IEntityTypeConfiguration<BaoCaoQuanTracMoiTruong>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoQuanTracMoiTruong> builder)
        {
            builder.ToTable("BaoCaoQuanTracMoiTruong");
            builder.HasKey(x => x.IdBaoCaoQuanTracMoiTruong);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsBaoCaoQuanTracMoiTruong).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
