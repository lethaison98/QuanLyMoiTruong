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
            migrationBuilder.DropForeignKey(
                name: "FK_DuAn_KhuCongNghiep_KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn");

            migrationBuilder.DropIndex(
                name: "IX_DuAn_KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn");

            migrationBuilder.DropColumn(
                name: "KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn");

            migrationBuilder.AlterColumn<int>(
                name: "IdKhuCongNghiep",
                table: "DuAn",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DuAn_IdKhuCongNghiep",
                table: "DuAn",
                column: "IdKhuCongNghiep");

            migrationBuilder.AddForeignKey(
                name: "FK_DuAn_KhuCongNghiep_IdKhuCongNghiep",
                table: "DuAn",
                column: "IdKhuCongNghiep",
                principalTable: "KhuCongNghiep",
                principalColumn: "IdKhuCongNghiep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DuAn_KhuCongNghiep_IdKhuCongNghiep",
                table: "DuAn");

            migrationBuilder.DropIndex(
                name: "IX_DuAn_IdKhuCongNghiep",
                table: "DuAn");

            migrationBuilder.AlterColumn<int>(
                name: "IdKhuCongNghiep",
                table: "DuAn",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DuAn_KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn",
                column: "KhuCongNghiepIdKhuCongNghiep");

            migrationBuilder.AddForeignKey(
                name: "FK_DuAn_KhuCongNghiep_KhuCongNghiepIdKhuCongNghiep",
                table: "DuAn",
                column: "KhuCongNghiepIdKhuCongNghiep",
                principalTable: "KhuCongNghiep",
                principalColumn: "IdKhuCongNghiep");
        }
    }
}
