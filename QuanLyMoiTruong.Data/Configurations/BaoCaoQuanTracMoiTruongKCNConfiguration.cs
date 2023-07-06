using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoQuanTracMoiTruongKCNConfiguration : IEntityTypeConfiguration<BaoCaoQuanTracMoiTruongKCN>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoQuanTracMoiTruongKCN> builder)
        {
            builder.ToTable("BaoCaoQuanTracMoiTruongKCN");
            builder.HasKey(x => x.IdBaoCaoQuanTracMoiTruongKCN);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsBaoCaoQuanTracMoiTruongKCN).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
