// using Microsoft.AspNetCore.Authentication.JwtBearer; 
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens; 
// using inmobiliariaApp.Application;
// using inmobiliariaApp.Domain.Interfaces;
// using inmobiliariaApp.Infrastructure.Data;
// using inmobiliariaApp.Infrastructure.Repositories;
// using System.Text;
// using inmobiliariaApp.Infrastructure.Extensions;
// using inmobiliariaApp.Infrastructure.Services;
// using inmobiliariaApp.Infrastructure.Settings;
// using ProductCatalog.Application.Interfaces;
// using System.Globalization;
//
// var builder = WebApplication.CreateBuilder(args);
//
// var cultureInfo = new CultureInfo("en-US");
// CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
// CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
// // -------------------------------------------------------------------
// // cloudinary
// builder.Services.Configure<CloudinarySettings>(
//     builder.Configuration.GetSection("CloudinarySettings")
// );
//
// builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
//
// // Connection with DB:
// builder.Services.AddInfrastructure(builder.Configuration);
//
//
// //  Dependence inyection (DI)
// builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
// builder.Services.AddScoped<PropertyService>();
//
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<UserService>();
//
//
// // Expose endpoints
// builder.Services.AddControllers(); 
// // -------------------------------------------------------------------
// //  Configuration  JWT
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
//             )
//         };
//     });
//
// //  Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// // -------------------------------------------------------------------
//
// var app = builder.Build();
//
//
// //deploy
// if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API v1");
//         c.RoutePrefix = string.Empty;
//     });
// }
//
// //  Middleware
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// // add autentication and authorization middlewares
// app.UseAuthentication();
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();
//
// // cambio

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using inmobiliariaApp.Application;
using inmobiliariaApp.Domain.Interfaces;
using inmobiliariaApp.Infrastructure.Data;
using inmobiliariaApp.Infrastructure.Repositories;
using System.Text;
using inmobiliariaApp.Infrastructure.Extensions;
using inmobiliariaApp.Infrastructure.Services;
using inmobiliariaApp.Infrastructure.Settings;
using ProductCatalog.Application.Interfaces;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------------
// Configuración de cultura
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// -------------------------------------------------------------------
// Cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings")
);

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// -------------------------------------------------------------------
// CORS (Importante para React y Heroku)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("ProdCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                //"https://tudominiofrontend.com",            // Reemplazar por nuestro dominio real en front
                "https://inmobiliaria-app-hu4-598dd1cade22.herokuapp.com"       // Dominio del backend
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// -------------------------------------------------------------------
// Conexión con la Base de Datos
builder.Services.AddInfrastructure(builder.Configuration);

// -------------------------------------------------------------------
// Dependencias
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

// Controladores
builder.Services.AddControllers();

// -------------------------------------------------------------------
// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// -------------------------------------------------------------------
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------------------------------------------------------
// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// -------------------------------------------------------------------
// Selección automática de CORS según entorno
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCorsPolicy");
}
else
{
    app.UseCors("ProdCorsPolicy");
}

// -------------------------------------------------------------------
app.UseHttpsRedirection();

// Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapControllers();

app.Run();
