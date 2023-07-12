using AdminBaker.Repositories.Implementations;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Implementations;
using AdminBaker.Services.Interfaces;
using AdminBaker.Services.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace AdminBaker.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddTransient<IClienteRepository, ClienteRepository>()
            .AddTransient<IMateriaPrimaRepository, MateriaPrimaRepository>()
            .AddTransient<IPedidoRepository, PedidoRepository>()
            .AddTransient<IProductoRepository, ProductoRepository>()
            .AddTransient<IRecetaRepository, RecetaRepository>()
            .AddTransient<ITipoTortaRepository, TipoTortaRepository>()
            .AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>()
            .AddTransient<IVendedorRepository, VendedorRepository>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddTransient<IClienteService, ClienteService>()
            .AddTransient<IMateriaPrimaService, MateriaPrimaService>()
            .AddTransient<IPedidoService, PedidoService>()
            .AddTransient<IProductoService, ProductoService>()
            .AddTransient<IRecetaService, RecetaService>()
            .AddTransient<ITipoTortaService, TipoTortaService>()
            .AddTransient<IUnidadMedidaService, UnidadMedidaService>()
            .AddTransient<IVendedorService, VendedorService>()
            .AddTransient<IFileUploader, AzureBlobStorageUploader>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IEmailService, EmailService>()
            .AddTransient<IPayPalTransactionService, PayPalTransactionService>();
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services.AddAutoMapper(p =>
        {
            p.AddProfile<MaestrosProfile>();
        });
    }
}