using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBaker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v600 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonPayPalResponse",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PedidoHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.Sql(@"CREATE PROCEDURE uspReporteTipoTortaTotal 
(@FechaInicio DATE,
@FechaFin DATE)
AS
BEGIN
	SELECT 
	TT.Nombre TipoTorta ,
	SUM(P.TotalVenta) SumaTotal
FROM
	PedidoITEM ITEM
INNER JOIN TipoTorta tt ON
	ITEM.TipoTortaId = TT.Id
INNER JOIN Pedido p ON
	ITEM.PedidoId = P.Id
WHERE
	P.EstadoPedido <> 4
	AND P.Fecha BETWEEN @FechaInicio AND @FechaFin
GROUP BY
	TT.Nombre
ORDER BY
	2
END 
GO 


CREATE PROCEDURE uspReporteCantidades
(@FechaInicio DATE,
@FechaFin DATE)
AS
BEGIN
	SELECT 
	(
	SELECT
		COUNT(PROD.ID)
	FROM
		Producto PROD
	WHERE
		PROD.Estado = 1) CantidadProductos,
	(
	SELECT
		COUNT(CLI.ID)
	FROM
		Cliente CLI
	WHERE
		CLI.Estado = 1) CantidadClientes,
	COUNT(P.Id) CantidadVentas,
	SUM(P.TotalVenta) SumaTotalVentas,
	AVG(P.TotalVenta) VentaPromedio
FROM
	Pedido P
WHERE
	P.EstadoPedido <> 4
	AND P.Fecha BETWEEN @FechaInicio AND @FechaFin
ORDER BY
	2
END 

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql("DROP PROCEDURE uspReporteTipoTortaTotal");
	        migrationBuilder.Sql("DROP PROCEDURE uspReporteCantidades");
	        
            migrationBuilder.DropColumn(
                name: "JsonPayPalResponse",
                table: "Pedido")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PedidoHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
