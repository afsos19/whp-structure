using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class alteracao_entidade_fraseologia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SAC_RESPOSTA_PRE_CADASTRADA");

            migrationBuilder.CreateTable(
                name: "FRASEOLOGIA_CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RESPOSTA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FRASEOLOGIA_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FRASEOLOGIA_ASSUNTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_CATEGORIA_FRASEOLOGIA = table.Column<int>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FRASEOLOGIA_ASSUNTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FRASEOLOGIA_ASSUNTO_FRASEOLOGIA_CATEGORIA_ID_CATEGORIA_FRASEOLOGIA",
                        column: x => x.ID_CATEGORIA_FRASEOLOGIA,
                        principalTable: "FRASEOLOGIA_CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FRASEOLOGIA_TIPO_ASSUNTO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_ASSUNTO_FRASEOLOGIA = table.Column<int>(nullable: true),
                    DESCRICAO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FRASEOLOGIA_TIPO_ASSUNTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FRASEOLOGIA_TIPO_ASSUNTO_FRASEOLOGIA_ASSUNTO_ID_ASSUNTO_FRASEOLOGIA",
                        column: x => x.ID_ASSUNTO_FRASEOLOGIA,
                        principalTable: "FRASEOLOGIA_ASSUNTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FRASEOLOGIA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RESPOSTA = table.Column<string>(nullable: true),
                    ATIVO = table.Column<bool>(nullable: false),
                    ID_TIPO_ASSUNTO_FRASEOLOGIA = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FRASEOLOGIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FRASEOLOGIA_FRASEOLOGIA_TIPO_ASSUNTO_ID_TIPO_ASSUNTO_FRASEOLOGIA",
                        column: x => x.ID_TIPO_ASSUNTO_FRASEOLOGIA,
                        principalTable: "FRASEOLOGIA_TIPO_ASSUNTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FRASEOLOGIA_ID_TIPO_ASSUNTO_FRASEOLOGIA",
                table: "FRASEOLOGIA",
                column: "ID_TIPO_ASSUNTO_FRASEOLOGIA");

            migrationBuilder.CreateIndex(
                name: "IX_FRASEOLOGIA_ASSUNTO_ID_CATEGORIA_FRASEOLOGIA",
                table: "FRASEOLOGIA_ASSUNTO",
                column: "ID_CATEGORIA_FRASEOLOGIA");

            migrationBuilder.CreateIndex(
                name: "IX_FRASEOLOGIA_TIPO_ASSUNTO_ID_ASSUNTO_FRASEOLOGIA",
                table: "FRASEOLOGIA_TIPO_ASSUNTO",
                column: "ID_ASSUNTO_FRASEOLOGIA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FRASEOLOGIA");

            migrationBuilder.DropTable(
                name: "FRASEOLOGIA_TIPO_ASSUNTO");

            migrationBuilder.DropTable(
                name: "FRASEOLOGIA_ASSUNTO");

            migrationBuilder.DropTable(
                name: "FRASEOLOGIA_CATEGORIA");

            migrationBuilder.CreateTable(
                name: "SAC_RESPOSTA_PRE_CADASTRADA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ATIVO = table.Column<bool>(nullable: false),
                    RESPOSTA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAC_RESPOSTA_PRE_CADASTRADA", x => x.ID);
                });
        }
    }
}
