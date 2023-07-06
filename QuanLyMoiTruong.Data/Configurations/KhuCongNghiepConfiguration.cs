using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class KhuCongNghiepConfiguration : IEntityTypeConfiguration<KhuCongNghiep>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KhuCongNghiep> builder)
        {
            builder.ToTable("KhuCongNghiep");
            builder.HasKey(x => x.IdKhuCongNghiep);
        }
    }
}
