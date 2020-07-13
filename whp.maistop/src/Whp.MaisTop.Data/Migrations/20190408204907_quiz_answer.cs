using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class quiz_answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QUESTIONARIO_USUARIO_RESPOSTA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_QUESTIONARIO_RESPOSTA = table.Column<int>(nullable: true),
                    RESPOSTA_DESCRITIVA = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONARIO_USUARIO_RESPOSTA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QUESTIONARIO_USUARIO_RESPOSTA_QUESTIONARIO_RESPOSTA_ID_QUESTIONARIO_RESPOSTA",
                        column: x => x.ID_QUESTIONARIO_RESPOSTA,
                        principalTable: "QUESTIONARIO_RESPOSTA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QUESTIONARIO_USUARIO_RESPOSTA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONARIO_USUARIO_RESPOSTA_ID_QUESTIONARIO_RESPOSTA",
                table: "QUESTIONARIO_USUARIO_RESPOSTA",
                column: "ID_QUESTIONARIO_RESPOSTA");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONARIO_USUARIO_RESPOSTA_ID_USUARIO",
                table: "QUESTIONARIO_USUARIO_RESPOSTA",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QUESTIONARIO_USUARIO_RESPOSTA");
        }
    }
}
