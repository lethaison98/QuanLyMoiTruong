using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoBaoVeMoiTruongKCNConfiguration : IEntityTypeConfiguration<BaoCaoBaoVeMoiTruongKCN>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoBaoVeMoiTruongKCN> builder)
        {
            builder.ToTable("BaoCaoBaoVeMoiTruongKCN");
            builder.HasKey(x => x.IdBaoCaoBaoVeMoiTruongKCN);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsBaoCaoBaoVeMoiTruongKCN).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
