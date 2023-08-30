using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class DiemQuanTracConfiguration : IEntityTypeConfiguration<DiemQuanTrac>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DiemQuanTrac> builder)
        {
            builder.ToTable("DiemQuanTrac");
            builder.HasKey(x => x.IdDiemQuanTrac);
        }
    }
}
