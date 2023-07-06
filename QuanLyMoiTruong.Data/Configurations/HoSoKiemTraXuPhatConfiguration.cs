using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class HoSoKiemTraXuPhatConfiguration : IEntityTypeConfiguration<HoSoKiemTraXuPhat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HoSoKiemTraXuPhat> builder)
        {
            builder.ToTable("HoSoKiemTraXuPhat");
            builder.HasKey(x => x.IdHoSoKiemTraXuPhat);
            builder.HasOne(x => x.DuAn).WithMany(hd => hd.DsHoSoKiemTraXuPhat).HasForeignKey(x => x.IdDuAn);
        }
    }
}
