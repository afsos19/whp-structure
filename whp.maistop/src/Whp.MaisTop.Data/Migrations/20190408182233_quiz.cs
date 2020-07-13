using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class quiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "QUESTIONARIO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVADO = table.Column<bool>(nullable: false),
                    IMAGEM = table.Column<string>(nullable: true),
                    NUMERO_MAXIMO_PERGUNTAS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONARIO_TIPO_PERGUNTA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRICAO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONARIO_TIPO_PERGUNTA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONARIO_PERGUNTA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_QUESTIONARIO = table.Column<int>(nullable: true),
                    ID_TIPO_PERGUNTA_QUESTIONARIO = table.Column<int>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true),
                    IMAGEM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONARIO_PERGUNTA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUESTIONARIO_PERGUNTA_QUESTIONARIO_TIPO_PERGUNTA_ID_TIPO_PERGUNTA_QUESTIONARIO",
                        column: x => x.ID_TIPO_PERGUNTA_QUESTIONARIO,
                        principalTable: "QUESTIONARIO_TIPO_PERGUNTA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QUESTIONARIO_PERGUNTA_QUESTIONARIO_ID_QUESTIONARIO",
                        column: x => x.ID_QUESTIONARIO,
                        principalTable: "QUESTIONARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONARIO_RESPOSTA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_QUESTIONARIO_PERGUNTA = table.Column<int>(nullable: true),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    DESCRICAO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONARIO_RESPOSTA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUESTIONARIO_RESPOSTA_QUESTIONARIO_PERGUNTA_ID_QUESTIONARIO_PERGUNTA",
                        column: x => x.ID_QUESTIONARIO_PERGUNTA,
                        principalTable: "QUESTIONARIO_PERGUNTA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONARIO_PERGUNTA_ID_TIPO_PERGUNTA_QUESTIONARIO",
                table: "QUESTIONARIO_PERGUNTA",
                column: "ID_TIPO_PERGUNTA_QUESTIONARIO");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONARIO_PERGUNTA_ID_QUESTIONARIO",
                table: "QUESTIONARIO_PERGUNTA",
                column: "ID_QUESTIONARIO");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONARIO_RESPOSTA_ID_QUESTIONARIO_PERGUNTA",
                table: "QUESTIONARIO_RESPOSTA",
                column: "ID_QUESTIONARIO_PERGUNTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QUESTIONARIO_RESPOSTA");

            migrationBuilder.DropTable(
                name: "QUESTIONARIO_PERGUNTA");

            migrationBuilder.DropTable(
                name: "QUESTIONARIO_TIPO_PERGUNTA");

            migrationBuilder.DropTable(
                name: "QUESTIONARIO");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FECHADO_EM",
                table: "SAC",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
