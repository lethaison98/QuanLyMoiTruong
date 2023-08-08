using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoBaoVeMoiTruongConfiguration : IEntityTypeConfiguration<BaoCaoBaoVeMoiTruong>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoBaoVeMoiTruong> builder)
        {
            builder.ToTable("BaoCaoBaoVeMoiTruong");
            builder.HasKey(x => x.IdBaoCaoBaoVeMoiTruong);
            builder.HasOne(x => x.DuAn).WithMany(hd => hd.DsBaoCaoBaoVeMoiTruong).HasForeignKey(x => x.IdDuAn);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsBaoCaoBaoVeMoiTruong).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
