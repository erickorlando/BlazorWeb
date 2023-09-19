# Admin Baker #

Proyecto realizado con ASP.NET Blazor 7.0


## Comprobar si se tiene instalado Entity Framework Core Tools

`dotnet ef`

_Deberia mostrarse el resultado siguiente:_

                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 7.0.11

## Instalar EF Core Tools de manera global en el equipo

`dotnet tool install dotnet-ef --global`

_La herramienta "dotnet-ef" ya está instalada._

## Actualizar EF Core Tools

`dotnet tool update dotnet-ef --global`

_La herramienta "dotnet-ef" se reinstaló con la versión estable más reciente (versión "7.0.11")._

## Pasos para ejecutar la aplicación

Abril la terminal y escribir

`dotnet ef database update --project .\AdminBaker.DataAccess\ --startup-project .\AdminBaker\Server\`

---------

## Valores de configuracion

Esta aplicacion utiliza los siguientes servicios:

- **Azure Blob Storage** para el almacenamiento de imagenes.
- **PayPal** Para la creacion de órdenes  de pago.
- **Envio de correos** a través de SMTP.

Este repositorio contiene valores no reales dentro del archivo `appsettings.Development.json` ubicado en el proyecto __Server__ del Blazor, por lo cual debes reemplazar los valores con los que necesites para que el proyecto funcione correctamente.

### Inicio de Sesion

De manera predeterminada, este proyecto crea un usuario administrador para acceder, para lo cual el usuario y clave son los siguientes:

| Usuario                | Clave        | Perfil        |
|------------------------|--------------|---------------|
| admin@adminbaker.cl    | Admin1234*   | Administrador |
| vendedor@adminbaker.cl | pa$$W0rD@123 | Vendedor      |

-----

# Disclaimer

Este proyecto no tiene soporte, es sólo un proyecto a nivel académico, úselo bajo su propio riesgo.

**[Erick Orlando © 2023](https://github.com/erickorlando)**