using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class new_related_campaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAMPANHAS_LOJAS_RELACIONADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_CAMPANHA_RELACIONADA = table.Column<int>(nullable: true),
                    ID_CAMPANHA = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPANHAS_LOJAS_RELACIONADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CAMPANHAS_LOJAS_RELACIONADA_CAMPANHAS_RELACIONADA_ID_CAMPANHA_RELACIONADA",
                        column: x => x.ID_CAMPANHA_RELACIONADA,
                        principalTable: "CAMPANHAS_RELACIONADA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAMPANHAS_LOJAS_RELACIONADA_LOJA_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "LOJA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_CAMPANHA_RELACIONADA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                column: "ID_CAMPANHA_RELACIONADA");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHAS_LOJAS_RELACIONADA_ID_CAMPANHA",
                table: "CAMPANHAS_LOJAS_RELACIONADA",
                column: "ID_CAMPANHA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAMPANHAS_LOJAS_RELACIONADA");
        }
    }
}
