using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiBaoCao",
                table: "BaoCaoBaoVeMoiTruong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KetQuaBaoVeMoiTruongDoanhNghiep",
                columns: table => new
                {
                    IdKetQuaBaoVeMoiTruongDoanhNghiep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBaoCaoBaoVeMoiTruong = table.Column<int>(type: "int", nullable: false),
                    IdDoanhNghiep = table.Column<int>(type: "int", nullable: false),
                    TenDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: false),
                    TenKhuCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoGiayTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHinhSanXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongLuongNuocThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DauNoiVaoHTXLNT = table.Column<bool>(type: "bit", nullable: false),
                    TachDauNoi = table.Column<bool>(type: "bit", nullable: false),
                    LuongKhiThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuanTracKhiThai = table.Column<bool>(type: "bit", nullable: false),
                    ChatThaiRanSinhHoat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanNguyHai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyLeCayXanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KetQuaBaoVeMoiTruongDoanhNghiep", x => x.IdKetQuaBaoVeMoiTruongDoanhNghiep);
                    table.ForeignKey(
                        name: "FK_KetQuaBaoVeMoiTruongDoanhNghiep_BaoCaoBaoVeMoiTruong_IdBaoCaoBaoVeMoiTruong",
                        column: x => x.IdBaoCaoBaoVeMoiTruong,
                        principalTable: "BaoCaoBaoVeMoiTruong",
                        principalColumn: "IdBaoCaoBaoVeMoiTruong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KetQuaBaoVeMoiTruongKCN",
                columns: table => new
                {
                    IdKetQuaBaoVeMoiTruongKCN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBaoCaoBaoVeMoiTruong = table.Column<int>(type: "int", nullable: false),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: false),
                    TenKhuCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChuDautu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuongCoSo = table.Column<int>(type: "int", nullable: false),
                    TyLeLapDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeThongThuGomNuocMua = table.Column<bool>(type: "bit", nullable: false),
                    TongLuongNuocThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongSuatThietKeHTXLNT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeThongQuanTracNuocThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanSinhHoat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanNguyHai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongTrinhPhongNgua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyLeCayXanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KetQuaBaoVeMoiTruongKCN", x => x.IdKetQuaBaoVeMoiTruongKCN);
                    table.ForeignKey(
                        name: "FK_KetQuaBaoVeMoiTruongKCN_BaoCaoBaoVeMoiTruong_IdBaoCaoBaoVeMoiTruong",
                        column: x => x.IdBaoCaoBaoVeMoiTruong,
                        principalTable: "BaoCaoBaoVeMoiTruong",
                        principalColumn: "IdBaoCaoBaoVeMoiTruong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaBaoVeMoiTruongDoanhNghiep_IdBaoCaoBaoVeMoiTruong",
                table: "KetQuaBaoVeMoiTruongDoanhNghiep",
                column: "IdBaoCaoBaoVeMoiTruong");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaBaoVeMoiTruongKCN_IdBaoCaoBaoVeMoiTruong",
                table: "KetQuaBaoVeMoiTruongKCN",
                column: "IdBaoCaoBaoVeMoiTruong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KetQuaBaoVeMoiTruongDoanhNghiep");

            migrationBuilder.DropTable(
                name: "KetQuaBaoVeMoiTruongKCN");

            migrationBuilder.DropColumn(
                name: "LoaiBaoCao",
                table: "BaoCaoBaoVeMoiTruong");
        }
    }
}
