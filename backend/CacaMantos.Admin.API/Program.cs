using backend.Application.Services;
using backend.Domain.IRepositories;
using backend.Domain.Services;
using backend.Domain.Services.IServices;
using backend.Infra.Data;
using backend.Infra.Data.Mapping.Entities;
using backend.Infra.Data.Repositories;
using CacaMantos.Admin.API.Application.Mapping.ToDomain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Serilog;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            try
            {

                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                                                .Enrich.FromLogContext()
                                                                .WriteTo.Console()
                                                                .CreateLogger();

                builder.Host.UseSerilog();

                RegistrarInfraestrutura(builder);

                builder.Services.AddScoped<ITimeService, TimeService>();
                builder.Services.AddScoped<ILojaService, LojaService>();
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
        }

        private static void RegistrarInfraestrutura(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ContextoBanco>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("BancoDados")));
            builder.Services.AddScoped<ILojaRepository, LojaRepository>();
            builder.Services.AddScoped<ITimeRepository, TimeRepository>();

            new TimeMap().Register(TypeAdapterConfig.GlobalSettings);
            new LojaMap().Register(TypeAdapterConfig.GlobalSettings);
            new PesquisaDTOMappings().Register(TypeAdapterConfig.GlobalSettings);
        }
    }
}