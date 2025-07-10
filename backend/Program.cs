using backend.Application;
using backend.Domain.IRepositories;
using backend.Domain.Services;
using backend.Domain.Services.IServices;
using backend.Infra.Data;
using backend.Infra.Data.Mongo.Mapping;
using backend.Infra.Data.Mongo.Repositories;
using backend.Infra.Data.Postgre;
using backend.Infra.Data.Postgre.Mapping.Entities;
using backend.Infra.Data.Postgre.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                                    .Enrich.FromLogContext()
                                                    .WriteTo.Console()
                                                    .CreateLogger();

    builder.Host.UseSerilog();

    RegistrarInfraPostgre(builder);
    //RegistrarInfraMongo(builder);

    builder.Services.AddScoped<ITimeService, TimeService>();
    builder.Services.AddScoped<TimeApplicationService>();
    builder.Services.AddScoped<LojaApplicationService>();

    builder.Services.AddMapster();
    builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
    builder.Services.AddScoped<IMapper, ServiceMapper>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "Dev", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });

    builder.Services.AddControllers();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.UseCors("Dev");

    //if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseRouting();
    app.UseHttpMetrics();

    app.MapControllers();


    app.MapMetrics();
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}

static void RegistrarInfraMongo(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton(sp => new ContextoBancoMongo());
    builder.Services.AddScoped<ITimeRepository, TimeRepositoryMongo>();
    builder.Services.AddScoped<ILojaRepository, LojaRepositoryMongo>();
    
    new TimeDocumentoMapping().Register(TypeAdapterConfig.GlobalSettings);
    new LojaDocumentoMapping().Register(TypeAdapterConfig.GlobalSettings);
}

static void RegistrarInfraPostgre(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ContextoBancoPostgres>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("postgres__ConnectionString")));
    builder.Services.AddScoped<ILojaRepository, LojaRepositoryPostgres>();
    builder.Services.AddScoped<ITimeRepository, TimeRepositoryPostgres>();
    
    new TimeMap().Register(TypeAdapterConfig.GlobalSettings);
    new LojaMap().Register(TypeAdapterConfig.GlobalSettings);
}