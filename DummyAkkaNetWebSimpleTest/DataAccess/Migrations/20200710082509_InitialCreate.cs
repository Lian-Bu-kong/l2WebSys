using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoilPDI",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoilScheduleID = table.Column<string>(nullable: true),
                    EntryCoilWidth = table.Column<float>(nullable: false),
                    EntryCoilLength = table.Column<float>(nullable: false),
                    EntryCoilWeight = table.Column<float>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoilPDI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoilSchedules",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SeqNo = table.Column<short>(nullable: false),
                    UpdateSource = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoilSchedules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoilPDI");

            migrationBuilder.DropTable(
                name: "CoilSchedules");
        }
    }
}
