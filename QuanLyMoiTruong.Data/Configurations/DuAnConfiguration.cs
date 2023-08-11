using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class DuAnConfiguration : IEntityTypeConfiguration<DuAn>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DuAn> builder)
        {
            builder.ToTable("DuAn");
            builder.HasKey(x => x.IdDuAn);
            builder.HasOne(x => x.KhuCongNghiep).WithMany(hd => hd.DsDuAn).HasForeignKey(x => x.IdKhuCongNghiep);

        }
    }
}
