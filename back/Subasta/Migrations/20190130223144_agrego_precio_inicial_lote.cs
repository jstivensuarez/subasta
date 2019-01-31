using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class agrego_precio_inicial_lote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRECIO_INICIAL_SUB",
                table: "TBL_SUBASTAS");

            migrationBuilder.AddColumn<decimal>(
                name: "PRECIO_INICIAL_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRECIO_INICIAL_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.AddColumn<decimal>(
                name: "PRECIO_INICIAL_SUB",
                table: "TBL_SUBASTAS",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
