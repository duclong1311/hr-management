using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CapNgay",
                table: "HoSos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "KetNapDang",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KetNapDoan",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KhenThuong",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoiCap",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoiKetNapDoan",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoiketNapDang",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SoThich",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TonGiao",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoVanHoa",
                table: "HoSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "CapNgay",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "KetNapDang",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "KetNapDoan",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "KhenThuong",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "NoiCap",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "NoiKetNapDoan",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "NoiketNapDang",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "SoThich",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "TonGiao",
                table: "HoSos");

            migrationBuilder.DropColumn(
                name: "TrinhDoVanHoa",
                table: "HoSos");
        }
    }
}
