using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class QuanHuyenConfiguration : IEntityTypeConfiguration<QuanHuyen>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<QuanHuyen> builder)
        {
            builder.ToTable("QuanHuyen");
            builder.HasKey(x => x.IdQuanHuyen);
            builder.HasOne(x => x.TinhThanh).WithMany(hd => hd.DSQuanHuyen).HasForeignKey(x => x.IdTinhThanh);
            builder.Property(x => x.IdQuanHuyen).ValueGeneratedNever();
        }
    }
}
