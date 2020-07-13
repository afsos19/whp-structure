using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class campaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAMPANHAS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITULO = table.Column<string>(nullable: true),
                    SUBTITULO = table.Column<string>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true),
                    FOTO = table.Column<string>(nullable: true),
                    FOTO_THUMB = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false),
                    ORDENACAO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPANHAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CAMPANHAS_RELACIONADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_CAMPANHA = table.Column<int>(nullable: true),
                    ID_CARGO = table.Column<int>(nullable: true),
                    ID_REDE = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPANHAS_RELACIONADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CAMPANHAS_RELACIONADA_CAMPANHAS_ID_CAMPANHA",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "CAMPANHAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAMPANHAS_RELACIONADA_REDE_ID_REDE",
                        column: x => x.ID_REDE,
                        principalTable: "REDE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAMPANHAS_RELACIONADA_CARGO_ID_CARGO",
                        column: x => x.ID_CARGO,
                        principalTable: "CARGO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHAS_RELACIONADA_ID_CAMPANHA",
                table: "CAMPANHAS_RELACIONADA",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHAS_RELACIONADA_ID_REDE",
                table: "CAMPANHAS_RELACIONADA",
                column: "ID_REDE");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPANHAS_RELACIONADA_ID_CARGO",
                table: "CAMPANHAS_RELACIONADA",
                column: "ID_CARGO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAMPANHAS_RELACIONADA");

            migrationBuilder.DropTable(
                name: "CAMPANHAS");
        }
    }
}
