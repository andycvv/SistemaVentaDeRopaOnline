using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVentaDeRopaOnline.Migrations
{
    /// <inheritdoc />
    public partial class AgregaratributoEstadodeInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Inventarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Inventarios");
        }
    }
}
