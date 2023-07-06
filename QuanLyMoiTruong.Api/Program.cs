using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using QuanLyMoiTruong.Api.Extensions;
using QuanLyMoiTruong.Data.EF;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddIdentity<AppUser, AppRole>()
               .AddEntityFrameworkStores<QuanLyMoiTruongDbContext>()
               .AddDefaultTokenProviders();
builder.Services.AddDbContext<QuanLyMoiTruongDbContext>
(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<QuanLyMoiTruongDbContext>();
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddInfrastructure();
builder.Services.AddSwaggerExtension();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
if (!Directory.Exists(Path.Combine(builder.Environment.ContentRootPath, "UploadFile")))
{
    Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "UploadFile"));
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "UploadFile")),
    RequestPath = "/UploadFile"
});
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
