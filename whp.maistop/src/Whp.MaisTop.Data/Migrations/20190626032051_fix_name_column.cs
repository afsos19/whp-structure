using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class fix_name_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAMPANHAS_LOJAS_RELACIONADA_LOJA_ID_CAMPANHA",
                table: "CAMPANHAS_LOJAS_RELACIONADA");

            migrationBuilder.RenameColumn(
                name: "ID_CAMPANHA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                newName: "ID_LOJA");

            migrationBuilder.RenameIndex(
                name: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_CAMPANHA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                newName: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_LOJA");

            migrationBuilder.AddForeignKey(
                name: "FK_CAMPANHAS_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAMPANHAS_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "CAMPANHAS_LOJAS_RELACIONADA");

            migrationBuilder.RenameColumn(
                name: "ID_LOJA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                newName: "ID_CAMPANHA");

            migrationBuilder.RenameIndex(
                name: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_LOJA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                newName: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_CAMPANHA");

            migrationBuilder.AddForeignKey(
                name: "FK_CAMPANHAS_LOJAS_RELACIONADA_LOJA_ID_CAMPANHA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                column: "ID_CAMPANHA",
                principalTable: "LOJA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
