using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBaker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v301 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "PedidoItem",
                type: "decimal(11,2)",
                precision: 11,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "PedidoItem");
        }
    }
}
