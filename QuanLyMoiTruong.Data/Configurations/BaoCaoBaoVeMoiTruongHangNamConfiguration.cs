using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class BaoCaoBaoVeMoiTruongHangNamConfiguration : IEntityTypeConfiguration<BaoCaoBaoVeMoiTruongHangNam>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BaoCaoBaoVeMoiTruongHangNam> builder)
        {
            builder.ToTable("BaoCaoBaoVeMoiTruongHangNam");
            builder.HasKey(x => x.IdBaoCaoBaoVeMoiTruongHangNam);
            builder.HasOne(x => x.DuAn).WithMany(hd => hd.DsBaoCaoBaoVeMoiTruongHangNam).HasForeignKey(x => x.IdDuAn);
        }
    }
}
