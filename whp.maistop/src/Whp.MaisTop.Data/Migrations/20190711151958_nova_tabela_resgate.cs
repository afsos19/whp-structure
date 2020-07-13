using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class nova_tabela_resgate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ESTORNADO_EM",
                table: "PEDIDO",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PEDIDO_EXTORNO_ITEM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_PEDIDO_ITEM = table.Column<int>(nullable: true),
                    ID_PEDIDO_EXTERNO = table.Column<long>(nullable: false),
                    ITEM_CODIGO = table.Column<int>(nullable: false),
                    MOTIVO = table.Column<string>(nullable: true),
                    VALOR_UNITARIO = table.Column<decimal>(nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(nullable: false),
                    QUANTIDADE = table.Column<int>(nullable: false),
                    CRIADO_EM = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_EXTORNO_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_EXTORNO_ITEM_PEDIDO_ITEM_ID_PEDIDO_ITEM",
                        column: x => x.ID_PEDIDO_ITEM,
                        principalTable: "PEDIDO_ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_EXTORNO_ITEM_ID_PEDIDO_ITEM",
                table: "PEDIDO_EXTORNO_ITEM",
                column: "ID_PEDIDO_ITEM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PEDIDO_EXTORNO_ITEM");

            migrationBuilder.DropColumn(
                name: "ESTORNADO_EM",
                table: "PEDIDO");
        }
    }
}
