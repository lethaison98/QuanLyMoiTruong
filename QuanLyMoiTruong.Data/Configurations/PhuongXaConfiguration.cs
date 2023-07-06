using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class PhuongXaConfiguration : IEntityTypeConfiguration<PhuongXa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhuongXa> builder)
        {
            builder.ToTable("PhuongXa");
            builder.HasKey(x => x.IdPhuongXa);
            builder.HasOne(x => x.QuanHuyen).WithMany(hd => hd.DSPhuongXa).HasForeignKey(x => x.IdQuanHuyen);
            builder.Property(x => x.IdPhuongXa).ValueGeneratedNever();

        }
    }
}
