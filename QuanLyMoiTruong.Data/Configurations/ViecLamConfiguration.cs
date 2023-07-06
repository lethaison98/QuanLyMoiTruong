using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Data.Configurations
{
    internal class ViecLamConfiguration : IEntityTypeConfiguration<ViecLam>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ViecLam> builder)
        {
            builder.ToTable("ViecLam");
            builder.HasKey(x => x.IdViecLam);
        }
    }
}
