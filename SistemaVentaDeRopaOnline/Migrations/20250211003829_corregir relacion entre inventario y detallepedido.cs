using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVentaDeRopaOnline.Migrations
{
    /// <inheritdoc />
    public partial class corregirrelacionentreinventarioydetallepedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Color_ColorId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Tallas_TallaId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_ColorId",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_ProductoId",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetallePedidos");

            migrationBuilder.RenameColumn(
                name: "TallaId",
                table: "DetallePedidos",
                newName: "InventarioId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_TallaId",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_InventarioId");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Color",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Inventarios_InventarioId",
                table: "DetallePedidos",
                column: "InventarioId",
                principalTable: "Inventarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Inventarios_InventarioId",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "InventarioId",
                table: "DetallePedidos",
                newName: "TallaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedidos_InventarioId",
                table: "DetallePedidos",
                newName: "IX_DetallePedidos_TallaId");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "DetallePedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetallePedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ColorId",
                table: "DetallePedidos",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ProductoId",
                table: "DetallePedidos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Color_ColorId",
                table: "DetallePedidos",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoId",
                table: "DetallePedidos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Tallas_TallaId",
                table: "DetallePedidos",
                column: "TallaId",
                principalTable: "Tallas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
