using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class updatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhenThuongs_NhanSus_NhanSuId",
                table: "KhenThuongs");

            migrationBuilder.DropIndex(
                name: "IX_KhenThuongs_NhanSuId",
                table: "KhenThuongs");

            migrationBuilder.DropColumn(
                name: "NhanSuId",
                table: "KhenThuongs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhanSuId",
                table: "KhenThuongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuongs_NhanSuId",
                table: "KhenThuongs",
                column: "NhanSuId");

            migrationBuilder.AddForeignKey(
                name: "FK_KhenThuongs_NhanSus_NhanSuId",
                table: "KhenThuongs",
                column: "NhanSuId",
                principalTable: "NhanSus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
