using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class upatev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UngLuongs_NhanSus_NhanSuId",
                table: "UngLuongs");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "UngLuongs");

            migrationBuilder.DropColumn(
                name: "MaNhanVien",
                table: "UngLuongs");

            migrationBuilder.AlterColumn<int>(
                name: "NhanSuId",
                table: "UngLuongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UngLuongs_NhanSus_NhanSuId",
                table: "UngLuongs",
                column: "NhanSuId",
                principalTable: "NhanSus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UngLuongs_NhanSus_NhanSuId",
                table: "UngLuongs");

            migrationBuilder.AlterColumn<int>(
                name: "NhanSuId",
                table: "UngLuongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "UngLuongs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNhanVien",
                table: "UngLuongs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UngLuongs_NhanSus_NhanSuId",
                table: "UngLuongs",
                column: "NhanSuId",
                principalTable: "NhanSus",
                principalColumn: "Id");
        }
    }
}
