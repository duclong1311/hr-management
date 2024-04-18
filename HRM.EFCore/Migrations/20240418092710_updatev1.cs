using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class updatev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuanHeGiaDinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTenCha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinhCha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgheNghiepCha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoQuanCongTacCha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChoOCha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTenMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinhMe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgheNghiepMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoQuanCongTacMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChoOMe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHeGiaDinhs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuanHeGiaDinhs");
        }
    }
}
