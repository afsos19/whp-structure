using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class user_access_expiration_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_CODIGO_EXPIRACAO_ACESSO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_USUARIO = table.Column<int>(nullable: true),
                    CODIGO = table.Column<string>(nullable: true),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_CODIGO_EXPIRACAO_ACESSO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_CODIGO_EXPIRACAO_ACESSO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_CODIGO_EXPIRACAO_ACESSO_ID_USUARIO",
                table: "USUARIO_CODIGO_EXPIRACAO_ACESSO",
                column: "ID_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_CODIGO_EXPIRACAO_ACESSO");
        }
    }
}
