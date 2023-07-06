using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class TinhThanhConfiguration : IEntityTypeConfiguration<TinhThanh>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TinhThanh> builder)
        {
            builder.ToTable("TinhThanh");
            builder.HasKey(x => x.IdTinhThanh);
            builder.Property(x => x.IdTinhThanh).ValueGeneratedNever();
        }
    }
}
