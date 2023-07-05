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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITipoTortaProxy, TipoTortaProxy>();

builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();
