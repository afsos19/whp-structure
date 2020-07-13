using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class new_quiz_related_and_quiz_shop_related : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QUIZ_RELACIONADO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_QUIZ = table.Column<int>(nullable: true),
                    ID_CARGO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUIZ_RELACIONADO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUIZ_RELACIONADO_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QUIZ_RELACIONADO_CARGO_ID_CARGO",
                        column: x => x.ID_CARGO,
                        principalTable: "CARGO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QUIZ_RELACIONADO_QUESTIONARIO_ID_QUIZ",
                        column: x => x.ID_QUIZ,
                        principalTable: "QUESTIONARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QUIZ_LOJAS_RELACIONADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_QUIZ_RELACIONADO = table.Column<int>(nullable: true),
                    ID_LOJA = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUIZ_LOJAS_RELACIONADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUIZ_LOJAS_RELACIONADA_QUIZ_RELACIONADO_ID_QUIZ_RELACIONADO",
                        column: x => x.ID_QUIZ_RELACIONADO,
                        principalTable: "QUIZ_RELACIONADO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QUIZ_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                        column: x => x.ID_LOJA,
                        principalTable: "LOJA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QUIZ_LOJAS_RELACIONADA_ID_QUIZ_RELACIONADO",
                table: "QUIZ_LOJAS_RELACIONADA",
                column: "ID_QUIZ_RELACIONADO");

            migrationBuilder.CreateIndex(
                name: "IX_QUIZ_LOJAS_RELACIONADA_ID_LOJA",
                table: "QUIZ_LOJAS_RELACIONADA",
                column: "ID_LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_QUIZ_RELACIONADO_ID_REDE",
                table: "QUIZ_RELACIONADO",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_QUIZ_RELACIONADO_ID_CARGO",
                table: "QUIZ_RELACIONADO",
                column: "ID_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_QUIZ_RELACIONADO_ID_QUIZ",
                table: "QUIZ_RELACIONADO",
                column: "ID_QUIZ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QUIZ_LOJAS_RELACIONADA");

            migrationBuilder.DropTable(
                name: "QUIZ_RELACIONADO");
        }
    }
}
