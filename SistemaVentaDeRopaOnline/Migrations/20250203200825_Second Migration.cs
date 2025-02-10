using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVentaDeRopaOnline.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Talla_TallaId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Talla_TallaId",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Talla",
                table: "Talla");

            migrationBuilder.RenameTable(
                name: "Talla",
                newName: "Tallas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tallas",
                table: "Tallas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Tallas_TallaId",
                table: "DetallePedidos",
                column: "TallaId",
                principalTable: "Tallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Tallas_TallaId",
                table: "Inventarios",
                column: "TallaId",
                principalTable: "Tallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Tallas_TallaId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Tallas_TallaId",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tallas",
                table: "Tallas");

            migrationBuilder.RenameTable(
                name: "Tallas",
                newName: "Talla");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Talla",
                table: "Talla",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Talla_TallaId",
                table: "DetallePedidos",
                column: "TallaId",
                principalTable: "Talla",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Talla_TallaId",
                table: "Inventarios",
                column: "TallaId",
                principalTable: "Talla",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
