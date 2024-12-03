using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Aplicacion;
using ServicioCorreo.Dal;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configuración de las capas adicionales (verifica que los servicios sean scoped o transient)
builder.Services.AddServicioDatos(builder.Configuration);
builder.Services.AddServicio(builder.Configuration);
builder.Services.AddServicioAplicacion(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<ContextCorreo>();
        await context.Database.MigrateAsync();
        //await ContextCorreoDatos.CargardatosAsincronos(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrió un error durante la migración de la base de datos.");
    }
}
app.Run();
