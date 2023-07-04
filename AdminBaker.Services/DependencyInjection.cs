using AdminBaker.Repositories.Implementations;
using AdminBaker.Repositories.Interfaces;
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
}