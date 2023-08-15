using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Application.Services;
using QuanLyMoiTruong.Data.EF;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddBusinessServices();
            services.AddUnitOfWork<QuanLyMoiTruongDbContext>();
            return services;
        }
        private static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IViecLamService, ViecLamService>();
            services.AddScoped<IDiaPhuongService, DiaPhuongService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileTaiLieuService, FileTaiLieuService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IDuAnService, DuAnService>();
            services.AddScoped<IBaoCaoBaoVeMoiTruongService, BaoCaoBaoVeMoiTruongService>();
            services.AddScoped<IGiayPhepMoiTruongService, GiayPhepMoiTruongService>();
            services.AddScoped<IHoSoKiemTraXuPhatService, HoSoKiemTraXuPhatService>();

            services.AddScoped<IKhuCongNghiepService, KhuCongNghiepService>();
            services.AddScoped<IBaoCaoQuanTracMoiTruongService, BaoCaoQuanTracMoiTruongService>();
            services.AddScoped<IThanhPhanMoiTruongService, ThanhPhanMoiTruongService>();
        }
    }  
}
