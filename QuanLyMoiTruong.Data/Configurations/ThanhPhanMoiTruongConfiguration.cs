using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class ThanhPhanMoiTruongConfiguration : IEntityTypeConfiguration<ThanhPhanMoiTruong>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ThanhPhanMoiTruong> builder)
        {
            builder.ToTable("ThanhPhanMoiTruong");
            builder.HasKey(x => x.IdThanhPhanMoiTruong);
        }
    }
}
