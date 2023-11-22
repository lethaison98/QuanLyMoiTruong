using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class KetQuaBaoVeMoiTruongKCNConfiguration : IEntityTypeConfiguration<KetQuaBaoVeMoiTruongKCN>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KetQuaBaoVeMoiTruongKCN> builder)
        {
            builder.ToTable("KetQuaBaoVeMoiTruongKCN");
            builder.HasKey(x => x.IdKetQuaBaoVeMoiTruongKCN);
            builder.HasOne(x => x.BaoCaoBaoVeMoiTruong).WithMany(hd => hd.DsKetQuaBaoVeMoiTruongKCN).HasForeignKey(x => x.IdBaoCaoBaoVeMoiTruong);
        }
    }
}
