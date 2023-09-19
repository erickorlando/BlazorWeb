using AdminBaker.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AdminBaker.DataAccess;

public static class UserDataSeeder
{
    public static async Task Seed(IServiceProvider service)
    {
        // UserManager (Repositorio de Usuarios)
        var userManager = service.GetRequiredService<UserManager<IdentityUserECommerce>>();
        // RoleManager (Repositorio de Roles)
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear Roles
        var adminRole = new IdentityRole(Constantes.RolAdministrador);
        var saleRole = new IdentityRole(Constantes.RolVendedor);
        var customerRole = new IdentityRole(Constantes.RolCliente);

        if (!await roleManager.RoleExistsAsync(Constantes.RolAdministrador))
            await roleManager.CreateAsync(adminRole);

        if (!await roleManager.RoleExistsAsync(Constantes.RolVendedor))
            await roleManager.CreateAsync(saleRole);

        if (!await roleManager.RoleExistsAsync(Constantes.RolCliente))
            await roleManager.CreateAsync(customerRole);

        var adminUser = new IdentityUserECommerce
        {
            Rut = "1-1",
            NombreCompleto = "Administrador del Sistema",
            UserName = "admin@adminbaker.cl",
            Email = "admin@adminbaker.cl",
            PhoneNumber = "+56 999 999 999",
            FechaNacimiento = DateTime.Parse("2000-01-01"),
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin1234*");
        if (result.Succeeded)
        {
            // Esto me asegura que el usuario se creó correctamente
            adminUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (adminUser is not null)
                await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
        }

        var saleUser = new IdentityUserECommerce()
        {
            Rut = "1-2",
            NombreCompleto = "Vendedor del Sistema",
            UserName = "vendedor@adminbaker.cl",
            Email = "vendedor@adminbaker.cl",
            PhoneNumber = "+56 999 999 999",
            FechaNacimiento = DateTime.Parse("2000-01-01"),
            EmailConfirmed = true
        };


        result = await userManager.CreateAsync(saleUser, "pa$$W0rD@123");
        if (result.Succeeded)
        {
            // Esto me asegura que el usuario se creó correctamente
            saleUser = await userManager.FindByEmailAsync(saleUser.Email);
            if (saleUser is not null)
                await userManager.AddToRoleAsync(saleUser, Constantes.RolVendedor);
        }

    }
}