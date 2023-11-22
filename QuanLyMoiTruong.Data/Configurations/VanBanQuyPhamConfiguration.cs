using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class VanBanQuyPhamConfiguration : IEntityTypeConfiguration<VanBanQuyPham>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VanBanQuyPham> builder)
        {
            builder.ToTable("VanBanQuyPham");
            builder.HasKey(x => x.IdVanBanQuyPham);        }
    }
}
