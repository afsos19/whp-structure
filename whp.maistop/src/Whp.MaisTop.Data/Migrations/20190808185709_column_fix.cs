using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class column_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RESPOSTA",
                table: "FRASEOLOGIA_CATEGORIA",
                newName: "NOME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "FRASEOLOGIA_CATEGORIA",
                newName: "RESPOSTA");
        }
    }
}
