using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class agrego_anticipo_lote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VALOR_ANTICIPO_SUB",
                table: "TBL_SUBASTAS");

            migrationBuilder.AddColumn<decimal>(
                name: "VALOR_ANTICIPO_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VALOR_ANTICIPO_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.AddColumn<decimal>(
                name: "VALOR_ANTICIPO_SUB",
                table: "TBL_SUBASTAS",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
