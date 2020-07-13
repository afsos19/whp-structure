using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class merg_develop_hml : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MENSAGEM_CATALOGO",
                table: "SAC_MENSAGEM",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "REDIRECIONADO_EM",
                table: "SAC",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RETORNADO_EM",
                table: "SAC",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CODIGO_ITEM",
                table: "PEDIDO_ITEM",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MENSAGEM_CATALOGO",
                table: "SAC_MENSAGEM");

            migrationBuilder.DropColumn(
                name: "REDIRECIONADO_EM",
                table: "SAC");

            migrationBuilder.DropColumn(
                name: "RETORNADO_EM",
                table: "SAC");

            migrationBuilder.AlterColumn<string>(
                name: "CODIGO_ITEM",
                table: "PEDIDO_ITEM",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
