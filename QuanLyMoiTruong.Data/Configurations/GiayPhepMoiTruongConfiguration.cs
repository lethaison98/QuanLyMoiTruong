using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class GiayPhepMoiTruongConfiguration : IEntityTypeConfiguration<GiayPhepMoiTruong>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GiayPhepMoiTruong> builder)
        {
            builder.ToTable("GiayPhepMoiTruong");
            builder.HasKey(x => x.IdGiayPhepMoiTruong);
            builder.HasOne(x => x.DuAn).WithMany(hd => hd.DsGiayPhepMoiTruong).HasForeignKey(x => x.IdDuAn);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsGiayPhepMoiTruong).HasForeignKey(x => x.IdKhuCongNghiep);
        }
    }
}
