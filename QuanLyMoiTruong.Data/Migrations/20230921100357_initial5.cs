using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaQuanTrac_IdDiemQuanTrac",
                table: "KetQuaQuanTrac",
                column: "IdDiemQuanTrac");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaQuanTrac_IdThanhPhanMoiTruong",
                table: "KetQuaQuanTrac",
                column: "IdThanhPhanMoiTruong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KetQuaQuanTrac");
        }
    }
}
