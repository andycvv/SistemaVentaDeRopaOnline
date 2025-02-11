using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVentaDeRopaOnline.Migrations
{
    /// <inheritdoc />
    public partial class agregarDbSetdeColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Color_ColorId",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colores",
                table: "Colores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Colores_ColorId",
                table: "Inventarios",
                column: "ColorId",
                principalTable: "Colores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Colores_ColorId",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colores",
                table: "Colores");

            migrationBuilder.RenameTable(
                name: "Colores",
                newName: "Color");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Color_ColorId",
                table: "Inventarios",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
