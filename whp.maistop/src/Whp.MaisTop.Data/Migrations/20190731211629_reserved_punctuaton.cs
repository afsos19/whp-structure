using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class reserved_punctuaton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PONTUACAO_USUARIO_RESERVADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PONTUACAO = table.Column<decimal>(nullable: false),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    ID_PEDIDO = table.Column<int>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PONTUACAO_USUARIO_RESERVADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PONTUACAO_USUARIO_RESERVADA_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PONTUACAO_USUARIO_RESERVADA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PONTUACAO_USUARIO_RESERVADA_ID_PEDIDO",
                table: "PONTUACAO_USUARIO_RESERVADA",
                column: "ID_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PONTUACAO_USUARIO_RESERVADA_ID_USUARIO",
                table: "PONTUACAO_USUARIO_RESERVADA",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PONTUACAO_USUARIO_RESERVADA");
        }
    }
}
