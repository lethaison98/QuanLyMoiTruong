using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoThongKeNguonThai_IdDuAn",
                table: "BaoCaoThongKeNguonThai",
                column: "IdDuAn");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoThongKeNguonThai_IdKhuCongNghiep",
                table: "BaoCaoThongKeNguonThai",
                column: "IdKhuCongNghiep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoCaoThongKeNguonThai");
        }
    }
}
