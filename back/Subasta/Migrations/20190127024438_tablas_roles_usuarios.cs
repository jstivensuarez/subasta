using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class tablas_roles_usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ROLES",
                columns: table => new
                {
                    CODIGO_ROL = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_ROL = table.Column<string>(nullable: true),
                    DESCRIPCION_ROL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ROLES", x => x.CODIGO_ROL);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    CODIGO_USU = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_USUARIO = table.Column<string>(nullable: true),
                    CORREO_USUARIO = table.Column<string>(nullable: true),
                    PASS_USUARIO = table.Column<string>(nullable: true),
                    ROL_USUARIO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.CODIGO_USU);
                    table.ForeignKey(
                        name: "FK_TBL_USUARIOS_TBL_ROLES_ROL_USUARIO",
                        column: x => x.ROL_USUARIO,
                        principalTable: "TBL_ROLES",
                        principalColumn: "CODIGO_ROL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USUARIOS_ROL_USUARIO",
                table: "TBL_USUARIOS",
                column: "ROL_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");

            migrationBuilder.DropTable(
                name: "TBL_ROLES");
        }
    }
}
