using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class closedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<DateTime>(
                name: "FECHADO_EM",
                table: "SAC",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FECHADO_EM",
                table: "SAC",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: false);
        }

    }
}
