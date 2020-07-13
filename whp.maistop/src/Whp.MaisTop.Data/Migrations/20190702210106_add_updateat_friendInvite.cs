using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class add_updateat_friendInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ATUALIZADO_EM",
                table: "USUARIO_CODIGO_CONVITE_ACESSO",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATUALIZADO_EM",
                table: "USUARIO_CODIGO_CONVITE_ACESSO");
        }
    }
}
