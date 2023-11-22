using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class KetQuaBaoVeMoiTruongDoanhNghiepConfiguration : IEntityTypeConfiguration<KetQuaBaoVeMoiTruongDoanhNghiep>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KetQuaBaoVeMoiTruongDoanhNghiep> builder)
        {
            builder.ToTable("KetQuaBaoVeMoiTruongDoanhNghiep");
            builder.HasKey(x => x.IdKetQuaBaoVeMoiTruongDoanhNghiep);
            builder.HasOne(x => x.BaoCaoBaoVeMoiTruong).WithMany(hd => hd.DsKetQuaBaoVeMoiTruongDoanhNghiep).HasForeignKey(x => x.IdBaoCaoBaoVeMoiTruong);
        }
    }
}
