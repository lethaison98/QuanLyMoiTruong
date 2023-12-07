using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMoiTruong.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuocGia",
                table: "DuAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TongVonDauTuVND",
                table: "DuAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiHoatDong",
                table: "DuAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDuAn",
                table: "DiemQuanTrac",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdKhuCongNghiep",
                table: "DiemQuanTrac",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KhuKinhTe",
                table: "DiemQuanTrac",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuocGia",
                table: "DuAn");

            migrationBuilder.DropColumn(
                name: "TongVonDauTuVND",
                table: "DuAn");

            migrationBuilder.DropColumn(
                name: "TrangThaiHoatDong",
                table: "DuAn");

            migrationBuilder.DropColumn(
                name: "IdDuAn",
                table: "DiemQuanTrac");

            migrationBuilder.DropColumn(
                name: "IdKhuCongNghiep",
                table: "DiemQuanTrac");

            migrationBuilder.DropColumn(
                name: "KhuKinhTe",
                table: "DiemQuanTrac");
        }
    }
}
