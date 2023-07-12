using System.Text;
using AdminBaker.DataAccess;
using AdminBaker.Entities.Configuration;
using AdminBaker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfig>(builder.Configuration);

// Add services to the container.
builder.Services.AddDbContext<AdminBakerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdminBakerDb"));
});

// Configuramos ASP.NET Identity
builder.Services.AddIdentity<IdentityUserECommerce, IdentityRole>(policies =>
    {
        policies.Password.RequireDigit = false;
        policies.Password.RequireLowercase = false;
        policies.Password.RequireUppercase = true;
        policies.Password.RequireNonAlphanumeric = true;
        policies.Password.RequiredLength = 8;

        policies.User.RequireUniqueEmail = true;

        // Politica de bloque de cuentas
        policies.Lockout.MaxFailedAccessAttempts = 5;
        policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        policies.Lockout.AllowedForNewUsers = true;
    })
    .AddEntityFrameworkStores<AdminBakerDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Admin Baker API",
        Version = "v1"
    });
});

builder.Services
    .AddRepositories()
    .AddMappers()
    .AddServices();

// Configuramos el contexto de seguridad del API
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                     throw new InvalidOperationException("No se configuro un SecretKey"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdminBaker API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Documentacion de la API";
        c.DocExpansion(DocExpansion.List);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
// Autenticacion
app.UseAuthentication();
// Autorizacion (permisos)
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    // Ejecutar la migracion
    var db = scope.ServiceProvider.GetRequiredService<AdminBakerDbContext>();
    db.Database.Migrate();
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();
