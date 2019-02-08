using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class agrego_campo_activo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ESTADO_CLI",
                table: "TBL_CLIENTES");

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVO_SUB",
                table: "TBL_SUBASTAS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVO_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVO_EVEN",
                table: "TBL_EVENTOS",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVO_CLI",
                table: "TBL_CLIENTES",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVO_ANI",
                table: "TBL_ANIMALES",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACTIVO_SUB",
                table: "TBL_SUBASTAS");

            migrationBuilder.DropColumn(
                name: "ACTIVO_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.DropColumn(
                name: "ACTIVO_EVEN",
                table: "TBL_EVENTOS");

            migrationBuilder.DropColumn(
                name: "ACTIVO_CLI",
                table: "TBL_CLIENTES");

            migrationBuilder.DropColumn(
                name: "ACTIVO_ANI",
                table: "TBL_ANIMALES");

            migrationBuilder.AddColumn<string>(
                name: "ESTADO_CLI",
                table: "TBL_CLIENTES",
                nullable: true);
        }
    }
}
