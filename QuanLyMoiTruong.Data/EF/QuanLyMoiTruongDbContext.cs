﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Data.Configurations;
using QuanLyMoiTruong.Data.Configurations;
using QuanLyMoiTruong.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace QuanLyMoiTruong.Data.EF
{
    public class QuanLyMoiTruongDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public IHttpContextAccessor _accessor { get; set; }
        public QuanLyMoiTruongDbContext(DbContextOptions options) : base(options)
        {
        }
        public QuanLyMoiTruongDbContext(DbContextOptions options, IHttpContextAccessor accessor) : base(options)
        {
            _accessor = accessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new PhuongXaConfiguration());
            modelBuilder.ApplyConfiguration(new QuanHuyenConfiguration());
            modelBuilder.ApplyConfiguration(new TinhThanhConfiguration());
            modelBuilder.ApplyConfiguration(new ViecLamConfiguration());

            modelBuilder.ApplyConfiguration(new FileTaiLieuConfiguration());
            modelBuilder.ApplyConfiguration(new FilesConfiguration());
            modelBuilder.ApplyConfiguration(new DuAnConfiguration());
            modelBuilder.ApplyConfiguration(new KhuCongNghiepConfiguration());
            modelBuilder.ApplyConfiguration(new BaoCaoBaoVeMoiTruongConfiguration());
            modelBuilder.ApplyConfiguration(new GiayPhepMoiTruongConfiguration());
            modelBuilder.ApplyConfiguration(new HoSoKiemTraXuPhatConfiguration());
            modelBuilder.ApplyConfiguration(new BaoCaoQuanTracMoiTruongConfiguration());
            modelBuilder.ApplyConfiguration(new ThanhPhanMoiTruongConfiguration());
            modelBuilder.ApplyConfiguration(new DiemQuanTracConfiguration());
            modelBuilder.ApplyConfiguration(new KetQuaQuanTracConfiguration());
            modelBuilder.ApplyConfiguration(new BaoCaoThongKeNguonThaiConfiguration());
            modelBuilder.ApplyConfiguration(new KetQuaThongKeNguonThaiConfiguration());
            modelBuilder.ApplyConfiguration(new KetQuaBaoVeMoiTruongDoanhNghiepConfiguration());
            modelBuilder.ApplyConfiguration(new KetQuaBaoVeMoiTruongKCNConfiguration());
            modelBuilder.ApplyConfiguration(new VanBanQuyPhamConfiguration());

        }
        public DbSet<AppConfig> AppConfig { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<PhuongXa> PhuongXa { get; set; }
        public DbSet<QuanHuyen> QuanHuyen { get; set; }
        public DbSet<TinhThanh> TinhThanh { get; set; }
        public DbSet<ViecLam> ViecLam { get; set; }


        public DbSet<DuAn> DuAn { get; set; }
        public DbSet<KhuCongNghiep> KhuCongNghiep { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FileTaiLieu> FileTaiLieu { get; set; }
        public DbSet<BaoCaoBaoVeMoiTruong> BaoCaoBaoVeMoiTruong { get; set; }
        public DbSet<GiayPhepMoiTruong> GiayPhepMoiTruong { get; set; }
        public DbSet<HoSoKiemTraXuPhat> HoSoKiemTraXuPhat { get; set; }
        public DbSet<BaoCaoQuanTracMoiTruong> BaoCaoQuanTracMoiTruong { get; set; }
        public DbSet<ThanhPhanMoiTruong> ThanhPhanMoiTruong { get; set; }
        public DbSet<DiemQuanTrac> DiemQuanTrac { get; set; }
        public DbSet<KetQuaQuanTrac> KetQuaQuanTrac { get; set; }
        public DbSet<BaoCaoThongKeNguonThai> BaoCaoThongKeNguonThai { get; set; }
        public DbSet<KetQuaThongKeNguonThai> KetQuaThongKeNguonThai { get; set; }
        public DbSet<KetQuaBaoVeMoiTruongDoanhNghiep> KetQuaBaoVeMoiTruongDoanhNghiep { get; set; }
        public DbSet<KetQuaBaoVeMoiTruongKCN> KetQuaBaoVeMoiTruongKCN { get; set; }
        public DbSet<VanBanQuyPham> VanBanQuyPham { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var claimsIdentity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("UserName")?.Value;
            var fullName = claimsIdentity.FindFirst("FullName")?.Value;
            var userId = claimsIdentity.FindFirst("UserId")?.Value;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.NguoiTao = userName + " - " + fullName;
                        entry.Entity.IdNguoiTao = userId;
                        entry.Entity.NgayTao = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.NguoiCapNhat = userName + " - " + fullName;
                        entry.Entity.NgayCapNhat = DateTime.Now;
                        entry.Entity.IdNguoiCapNhat = userId;
                        break;
                }
            }
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
