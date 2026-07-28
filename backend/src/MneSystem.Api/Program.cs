using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MneSystem.Api.Configuration;
using MneSystem.Api.Middleware;
using MneSystem.Infrastructure;
using MneSystem.Infrastructure.Data.Seeding;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Starting M&E System API");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddCorsConfiguration();

    var app = builder.Build();

    await ApplicationDbContextSeeder.SeedDatabaseAsync(app.Services);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerConfiguration();
    }

    app.UseGlobalExceptionMiddleware();
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("M&E System API started successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}