using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class policy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "POLITICA_PRIVACIDADE",
                table: "USUARIO",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_PREVISAO",
                table: "PEDIDO",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POLITICA_PRIVACIDADE",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "DATA_PREVISAO",
                table: "PEDIDO");
        }
    }
}
