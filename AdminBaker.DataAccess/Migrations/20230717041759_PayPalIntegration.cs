using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBaker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PayPalIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                    name: "PayPalUrlOrder",
                    table: "Pedido",
                    type: "nvarchar(max)",
                    nullable: true)
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PedidoHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.Sql(
                @"CREATE OR ALTER PROCEDURE uspListarPedidos(@FechaInicio DATE, @FechaFin DATE, @Filter NVARCHAR(100) = NULL) AS
BEGIN
	SELECT
		p.Id,
		p.Fecha ,
		p.UrlImagen ,
		item.Tamanio,
		item.Relleno,
		item.TipoTorta,
		item.Precio,
		item.Cantidad ,
		c.Id ClienteId,
		c.NombreCompleto Cliente,
		c.Direccion ,
		c.Rut ,
		v.Id VendedorId,
		v.NombreCompleto Vendedor,
		p.TotalVenta,
		p.FechaRetiro ,
		p.NroPedido,
		p.MensajePersonalizado,
		p.EstadoPedido,
		p.JsonPayPalResponse ,
		p.PayPalUrlOrder,
		'Pedido Especial' Origen
	FROM
		Pedido p
	CROSS APPLY (
		SELECT
			TOP 1 i.ProductoId ,
			i.Tamanio,
			i.Relleno,
			i.Cantidad,
			tt.Nombre TipoTorta,
			prod.Precio
		FROM
			PedidoItem i
		INNER JOIN Producto prod ON
			i.ProductoId = prod.Id
		INNER JOIN TipoTorta tt ON
			i.TipoTortaId = tt.Id
		WHERE
			p.Id = i.PedidoId) Item
	LEFT JOIN Cliente c ON
		c.Id = p.ClienteId
	LEFT JOIN Vendedor v ON
		v.Id = p.VendedorId
	WHERE p.TipoPedido = 1
	AND p.Fecha BETWEEN @FechaInicio AND @FechaFin
	AND (@Filter IS NULL OR (C.Rut LIKE @Filter + '%' OR C.NombreCompleto LIKE '%' + @Filter + '%'))
	UNION ALL
	SELECT 
		p.Id,
		p.Fecha ,
		p.UrlImagen ,
		item.Tamanio,
		item.Relleno,
		tt.Nombre TipoTorta ,
		item.PrecioUnitario Precio,
		item.Cantidad ,
		c.Id ClienteId,
		c.NombreCompleto Cliente,
		c.Direccion ,
		c.Rut ,
		v.Id VendedorId,
		v.NombreCompleto Vendedor,
		p.TotalVenta,
		p.FechaRetiro ,
		p.NroPedido,
		p.MensajePersonalizado,
		p.EstadoPedido,
		p.JsonPayPalResponse ,
		p.PayPalUrlOrder,
		'Carrito' Origen
	FROM
		Pedido p
	INNER JOIN PedidoItem item ON
		P.Id = item.PedidoId
	INNER JOIN TipoTorta tt ON
		TT.Id = item.TipoTortaId
	LEFT JOIN Cliente c ON
		c.Id = p.ClienteId
	LEFT JOIN Vendedor v ON
		v.Id = p.VendedorId
	WHERE p.TipoPedido = 0
	AND p.Fecha BETWEEN @FechaInicio AND @FechaFin
	AND (@Filter IS NULL OR (C.Rut LIKE @Filter + '%' OR C.NombreCompleto LIKE '%' + @Filter + '%'))
	ORDER BY Fecha DESC
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayPalUrlOrder",
                table: "Pedido")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "PedidoHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        }
    }
}
