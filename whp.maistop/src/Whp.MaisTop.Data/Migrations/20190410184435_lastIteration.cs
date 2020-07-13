using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class lastIteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ULTIMA_ITERACAO",
                table: "SAC",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ULTIMA_ITERACAO",
                table: "SAC");
        }
    }
}
