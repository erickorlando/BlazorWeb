CREATE OR ALTER PROC uspAuditoriaPedidos
AS
BEGIN
	-- PEDIDOS
SELECT
	TOP 100 [p].[Id],
	[p].[NroPedido],
	[p].[Fecha],
	[c].[NombreCompleto] AS [Cliente],
	CASE
		[p].[EstadoPedido] 
WHEN 0 THEN 'Pendiente'
		WHEN 1 THEN 'En Preparación'
		WHEN 2 THEN 'En Camino'
		WHEN 3 THEN 'Entregado'
	END AS EstadoPedido
,
	[v].[NombreCompleto] AS [Vendedor],
	P.Usuario
,
	[p].[PeriodEnd] AS [FechaCambio]
FROM
	[PedidoHistory] AS [p]
INNER JOIN [Cliente] AS [c] ON
	[p].[ClienteId] = [c].[Id]
LEFT JOIN [Vendedor] AS [v] ON
	[p].[VendedorId] = [v].[Id]
ORDER BY
	P.PeriodEnd DESC
END
GO

CREATE OR ALTER PROCEDURE uspAuditoriaProductos
AS 
BEGIN
-- PRODUCTOS
SELECT
	TOP 50
	P.Id,
	P.Nombre,
	P.Cantidad,
	P.Precio,
	P.Relleno,
	TT.Nombre TipoTorta,
	P.Tamanio,
	CASE
		P.Estado 
	WHEN 1 THEN 'Activo'
		WHEN 0 THEN 'Borrado'
	END Estado,
	P.Usuario,
	P.PeriodEnd FechaCambio
FROM
	ProductoHistory P
LEFT JOIN TipoTorta TT ON
	TT.Id = P.TipoTortaId
ORDER BY
	P.PeriodEnd DESC
END
GO 

CREATE OR ALTER PROCEDURE uspAuditoriaMateriaPrimas
AS 
BEGIN
-- MATERIAS PRIMA
SELECT
	TOP 50
	MP.Id,
	MP.Nombre,
	MP.Caducidad,
	MP.Cantidad,
	UM.Descripcion UnidadMedida,
	CASE
		MP.Estado 
	WHEN 1 THEN 'Activo'
		WHEN 0 THEN 'Borrado'
	END Estado,
	MP.PeriodEnd AS FechaCambio,
	MP.Usuario
FROM
	MateriaPrimaHistory MP
INNER JOIN UnidadMedida UM ON
	UM.Id = MP.Id
ORDER BY
	MP.PeriodEnd DESC
END

GO 

CREATE OR ALTER PROCEDURE uspAuditoriaClientes
AS 
BEGIN
--CLIENTES
SELECT
	TOP 50
	C.ID,
	C.NombreCompleto,
	C.Rut,
	C.Email,
	C.Direccion,
	C.FechaNacimiento,
	CASE
		C.Estado 
	WHEN 1 THEN 'Activo'
		WHEN 0 THEN 'Borrado'
	END Estado,
	C.Usuario,
	C.PeriodEnd FechaCambio
FROM
	ClienteHistory C
ORDER BY
	C.PeriodEnd DESC
END 
GO 

CREATE OR ALTER PROCEDURE uspAuditoriaVendedores
AS
BEGIN
--VENDEDOR
SELECT
	TOP 50
	V.ID,
	V.NombreCompleto,
	V.Rut,
	V.Email,
	V.Direccion,
	V.Horario,
	CASE
		V.Estado 
	WHEN 1 THEN 'Activo'
		WHEN 0 THEN 'Borrado'
	END Estado,
	V.Usuario,
	V.PeriodEnd FechaCambio
FROM
	VendedorHistory V
ORDER BY
	V.PeriodEnd DESC
END 
GO



DROP PROCEDURE uspAuditoriaPedidos
GO 
DROP PROCEDURE uspAuditoriaProductos
GO 
DROP PROCEDURE uspAuditoriaMateriaPrimas
GO 
DROP PROCEDURE uspAuditoriaClientes
GO 
DROP PROCEDURE uspAuditoriaVendedores