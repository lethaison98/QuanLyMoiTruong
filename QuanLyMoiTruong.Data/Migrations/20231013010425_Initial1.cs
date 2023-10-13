using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Piority = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DiemQuanTrac",
                columns: table => new
                {
                    IdDiemQuanTrac = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiemQuanTrac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KinhDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemQuanTrac", x => x.IdDiemQuanTrac);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    IdFile = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.IdFile);
                });

            migrationBuilder.CreateTable(
                name: "KhuCongNghiep",
                columns: table => new
                {
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhuCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChuDauTu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuocKhuKinhTe = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuCongNghiep", x => x.IdKhuCongNghiep);
                });

            migrationBuilder.CreateTable(
                name: "ThanhPhanMoiTruong",
                columns: table => new
                {
                    IdThanhPhanMoiTruong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThanhPhanMoiTruong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhPhanMoiTruong", x => x.IdThanhPhanMoiTruong);
                });

            migrationBuilder.CreateTable(
                name: "TinhThanh",
                columns: table => new
                {
                    IdTinhThanh = table.Column<int>(type: "int", nullable: false),
                    TenTinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinhThanh", x => x.IdTinhThanh);
                });

            migrationBuilder.CreateTable(
                name: "ViecLam",
                columns: table => new
                {
                    IdViecLam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeuCau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyenLoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucLuongToiThieu = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MucLuongToiDa = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DonViTienTe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViThoiGian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuyenDungTuNgay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TuyenDungDenNgay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThongTinNhaTuyenDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSIdDiaPhuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSIdNganhNghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSIdPhucLoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    MucUuTien = table.Column<int>(type: "int", nullable: false),
                    XepHang = table.Column<int>(type: "int", nullable: false),
                    NgayPheDuyet = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserPheDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViecLam", x => x.IdViecLam);
                });

            migrationBuilder.CreateTable(
                name: "FileTaiLieu",
                columns: table => new
                {
                    IdFileTaiLieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFile = table.Column<int>(type: "int", nullable: false),
                    IdTaiLieu = table.Column<int>(type: "int", nullable: false),
                    NhomTaiLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiFileTaiLieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTaiLieu", x => x.IdFileTaiLieu);
                    table.ForeignKey(
                        name: "FK_FileTaiLieu_Files_IdFile",
                        column: x => x.IdFile,
                        principalTable: "Files",
                        principalColumn: "IdFile",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DuAn",
                columns: table => new
                {
                    IdDuAn = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDuAn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    ThuocKhuKinhTe = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNguoiPhuTrachTNMT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiayPhepDKKD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHinhSanXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyMo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuAn", x => x.IdDuAn);
                    table.ForeignKey(
                        name: "FK_DuAn_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "KetQuaQuanTrac",
                columns: table => new
                {
                    IdKetQuaQuanTrac = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDiemQuanTrac = table.Column<int>(type: "int", nullable: false),
                    IdThanhPhanMoiTruong = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    ChiTieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuChuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguongToiThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguongToiDa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuaQuanTrac", x => x.IdKetQuaQuanTrac);
                    table.ForeignKey(
                        name: "FK_KetQuaQuanTrac_DiemQuanTrac_IdDiemQuanTrac",
                        column: x => x.IdDiemQuanTrac,
                        principalTable: "DiemQuanTrac",
                        principalColumn: "IdDiemQuanTrac",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KetQuaQuanTrac_ThanhPhanMoiTruong_IdThanhPhanMoiTruong",
                        column: x => x.IdThanhPhanMoiTruong,
                        principalTable: "ThanhPhanMoiTruong",
                        principalColumn: "IdThanhPhanMoiTruong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuanHuyen",
                columns: table => new
                {
                    IdQuanHuyen = table.Column<int>(type: "int", nullable: false),
                    TenQuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTinhThanh = table.Column<int>(type: "int", nullable: false),
                    TenTinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHuyen", x => x.IdQuanHuyen);
                    table.ForeignKey(
                        name: "FK_QuanHuyen_TinhThanh_IdTinhThanh",
                        column: x => x.IdTinhThanh,
                        principalTable: "TinhThanh",
                        principalColumn: "IdTinhThanh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoBaoVeMoiTruong",
                columns: table => new
                {
                    IdBaoCaoBaoVeMoiTruong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDuAn = table.Column<int>(type: "int", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    KhuKinhTe = table.Column<bool>(type: "bit", nullable: false),
                    TenBaoCao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoBaoVeMoiTruong", x => x.IdBaoCaoBaoVeMoiTruong);
                    table.ForeignKey(
                        name: "FK_BaoCaoBaoVeMoiTruong_DuAn_IdDuAn",
                        column: x => x.IdDuAn,
                        principalTable: "DuAn",
                        principalColumn: "IdDuAn");
                    table.ForeignKey(
                        name: "FK_BaoCaoBaoVeMoiTruong_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoQuanTracMoiTruong",
                columns: table => new
                {
                    IdBaoCaoQuanTracMoiTruong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDuAn = table.Column<int>(type: "int", nullable: true),
                    DuAnIdDuAn = table.Column<int>(type: "int", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    TenBaoCao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoQuanTracMoiTruong", x => x.IdBaoCaoQuanTracMoiTruong);
                    table.ForeignKey(
                        name: "FK_BaoCaoQuanTracMoiTruong_DuAn_DuAnIdDuAn",
                        column: x => x.DuAnIdDuAn,
                        principalTable: "DuAn",
                        principalColumn: "IdDuAn");
                    table.ForeignKey(
                        name: "FK_BaoCaoQuanTracMoiTruong_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoThongKeNguonThai",
                columns: table => new
                {
                    IdBaoCaoThongKeNguonThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBaoCao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    IdDuAn = table.Column<int>(type: "int", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    KhuKinhTe = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoThongKeNguonThai", x => x.IdBaoCaoThongKeNguonThai);
                    table.ForeignKey(
                        name: "FK_BaoCaoThongKeNguonThai_DuAn_IdDuAn",
                        column: x => x.IdDuAn,
                        principalTable: "DuAn",
                        principalColumn: "IdDuAn");
                    table.ForeignKey(
                        name: "FK_BaoCaoThongKeNguonThai_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "GiayPhepMoiTruong",
                columns: table => new
                {
                    IdGiayPhepMoiTruong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDuAn = table.Column<int>(type: "int", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    SoGiayPhep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenGiayPhep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoQuanCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiayPhepMoiTruong", x => x.IdGiayPhepMoiTruong);
                    table.ForeignKey(
                        name: "FK_GiayPhepMoiTruong_DuAn_IdDuAn",
                        column: x => x.IdDuAn,
                        principalTable: "DuAn",
                        principalColumn: "IdDuAn");
                    table.ForeignKey(
                        name: "FK_GiayPhepMoiTruong_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "HoSoKiemTraXuPhat",
                columns: table => new
                {
                    IdHoSoKiemTraXuPhat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDuAn = table.Column<int>(type: "int", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: true),
                    TenHoSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Lan = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiTao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNguoiCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKiemTraXuPhat", x => x.IdHoSoKiemTraXuPhat);
                    table.ForeignKey(
                        name: "FK_HoSoKiemTraXuPhat_DuAn_IdDuAn",
                        column: x => x.IdDuAn,
                        principalTable: "DuAn",
                        principalColumn: "IdDuAn");
                    table.ForeignKey(
                        name: "FK_HoSoKiemTraXuPhat_KhuCongNghiep_IdKhuCongNghiep",
                        column: x => x.IdKhuCongNghiep,
                        principalTable: "KhuCongNghiep",
                        principalColumn: "IdKhuCongNghiep");
                });

            migrationBuilder.CreateTable(
                name: "PhuongXa",
                columns: table => new
                {
                    IdPhuongXa = table.Column<int>(type: "int", nullable: false),
                    TenPhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdQuanHuyen = table.Column<int>(type: "int", nullable: false),
                    TenQuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTinhThanh = table.Column<int>(type: "int", nullable: false),
                    TenTinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongXa", x => x.IdPhuongXa);
                    table.ForeignKey(
                        name: "FK_PhuongXa_QuanHuyen_IdQuanHuyen",
                        column: x => x.IdQuanHuyen,
                        principalTable: "QuanHuyen",
                        principalColumn: "IdQuanHuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoBaoVeMoiTruong_IdDuAn",
                table: "BaoCaoBaoVeMoiTruong",
                column: "IdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoBaoVeMoiTruong_IdKhuCongNghiep",
                table: "BaoCaoBaoVeMoiTruong",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoQuanTracMoiTruong_DuAnIdDuAn",
                table: "BaoCaoQuanTracMoiTruong",
                column: "DuAnIdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoQuanTracMoiTruong_IdKhuCongNghiep",
                table: "BaoCaoQuanTracMoiTruong",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoThongKeNguonThai_IdDuAn",
                table: "BaoCaoThongKeNguonThai",
                column: "IdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoThongKeNguonThai_IdKhuCongNghiep",
                table: "BaoCaoThongKeNguonThai",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_DuAn_IdKhuCongNghiep",
                table: "DuAn",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_FileTaiLieu_IdFile",
                table: "FileTaiLieu",
                column: "IdFile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiayPhepMoiTruong_IdDuAn",
                table: "GiayPhepMoiTruong",
                column: "IdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_GiayPhepMoiTruong_IdKhuCongNghiep",
                table: "GiayPhepMoiTruong",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoKiemTraXuPhat_IdDuAn",
                table: "HoSoKiemTraXuPhat",
                column: "IdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoKiemTraXuPhat_IdKhuCongNghiep",
                table: "HoSoKiemTraXuPhat",
                column: "IdKhuCongNghiep");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaQuanTrac_IdDiemQuanTrac",
                table: "KetQuaQuanTrac",
                column: "IdDiemQuanTrac");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaQuanTrac_IdThanhPhanMoiTruong",
                table: "KetQuaQuanTrac",
                column: "IdThanhPhanMoiTruong");

            migrationBuilder.CreateIndex(
                name: "IX_PhuongXa_IdQuanHuyen",
                table: "PhuongXa",
                column: "IdQuanHuyen");

            migrationBuilder.CreateIndex(
                name: "IX_QuanHuyen_IdTinhThanh",
                table: "QuanHuyen",
                column: "IdTinhThanh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfig");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "BaoCaoBaoVeMoiTruong");

            migrationBuilder.DropTable(
                name: "BaoCaoQuanTracMoiTruong");

            migrationBuilder.DropTable(
                name: "BaoCaoThongKeNguonThai");

            migrationBuilder.DropTable(
                name: "FileTaiLieu");

            migrationBuilder.DropTable(
                name: "GiayPhepMoiTruong");

            migrationBuilder.DropTable(
                name: "HoSoKiemTraXuPhat");

            migrationBuilder.DropTable(
                name: "KetQuaQuanTrac");

            migrationBuilder.DropTable(
                name: "PhuongXa");

            migrationBuilder.DropTable(
                name: "ViecLam");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "DuAn");

            migrationBuilder.DropTable(
                name: "DiemQuanTrac");

            migrationBuilder.DropTable(
                name: "ThanhPhanMoiTruong");

            migrationBuilder.DropTable(
                name: "QuanHuyen");

            migrationBuilder.DropTable(
                name: "KhuCongNghiep");

            migrationBuilder.DropTable(
                name: "TinhThanh");
        }
    }
}
