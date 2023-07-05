using AdminBaker.Client;
using AdminBaker.Client.Proxy;
using AdminBaker.Client.Proxy.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITipoTortaProxy, TipoTortaProxy>();
builder.Services.AddScoped<IUnidadMedidaProxy, UnidadMedidaProxy>();
builder.Services.AddScoped<IVendedorProxy, VendedorProxy>();
builder.Services.AddScoped<IProductoProxy, ProductoProxy>();
builder.Services.AddScoped<IClienteProxy, ClienteProxy>();
builder.Services.AddScoped<IPedidoProxy, PedidoProxy>();
builder.Services.AddScoped<IMateriaPrimaProxy, MateriaPrimaProxy>();
builder.Services.AddScoped<IRecetaProxy, RecetaProxy>();

builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ICarritoServicio, CarritoServicio>();


await builder.Build().RunAsync();
