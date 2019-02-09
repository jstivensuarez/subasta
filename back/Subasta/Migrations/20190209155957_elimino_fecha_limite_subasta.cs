using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class elimino_fecha_limite_subasta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FECHA_HORA_INS_SUB",
                table: "TBL_SUBASTAS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FECHA_HORA_INS_SUB",
                table: "TBL_SUBASTAS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
