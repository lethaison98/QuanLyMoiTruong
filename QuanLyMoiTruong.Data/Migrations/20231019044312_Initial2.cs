using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KetQuaThongKeNguonThai",
                columns: table => new
                {
                    IdKetQuaThongKeNguonThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBaoCaoThongKeNguonThai = table.Column<int>(type: "int", nullable: false),
                    IdKhuCongNghiep = table.Column<int>(type: "int", nullable: false),
                    TenKhuCongNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NuocThaiSinhHoat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NuocThaiSanXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NuocThaiTaiSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuuLuongDauNoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhiThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanSinhHoat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanSanXuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiRanTaiSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongChatThaiRan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatThaiNguyHai = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_KetQuaThongKeNguonThai", x => x.IdKetQuaThongKeNguonThai);
                    table.ForeignKey(
                        name: "FK_KetQuaThongKeNguonThai_BaoCaoThongKeNguonThai_IdBaoCaoThongKeNguonThai",
                        column: x => x.IdBaoCaoThongKeNguonThai,
                        principalTable: "BaoCaoThongKeNguonThai",
                        principalColumn: "IdBaoCaoThongKeNguonThai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KetQuaThongKeNguonThai_IdBaoCaoThongKeNguonThai",
                table: "KetQuaThongKeNguonThai",
                column: "IdBaoCaoThongKeNguonThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KetQuaThongKeNguonThai");
        }
    }
}
