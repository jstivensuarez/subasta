using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class agrego_descripcion_subasta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DESCRIPCION_SUB",
                table: "TBL_SUBASTAS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DESCRIPCION_SUB",
                table: "TBL_SUBASTAS");
        }
    }
}
