using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfGenerator.Application.PdfConversions;
using PdfGenerator.Application.PdfConversions.Profiles;
using PdfGenerator.FileStorage.Extensions;
using PdfGenerator.Infrastructure.DataAccess;
using PdfGenerator.Infrastructure.Extensions;
using PdfGenerator.Observability.Extensions;
using PdfGenerator.Shared.Exceptions;
using Serilog;

namespace PdfGenerator.Worker;

public class PdfGeneratorWorker(string[] args)
{
    protected HostApplicationBuilder Builder { get; } = Host.CreateEmptyApplicationBuilder(
        new HostApplicationBuilderSettings
        {
            Args = args
        });

    public async Task RunAsync()
    {
        Builder.Configuration.AddJsonFile("appsettings.json", false, true);
        Builder.RegisterSerilogLogger();
        
        var hangfireConnectionString = Builder.Configuration.GetConnectionString("Hangfire");
        _ = hangfireConnectionString ?? throw new RequiredConfigNotDefined("ConnectionStrings:Hangfire");
        
        var pdfGeneratorConnectionString = Builder.Configuration.GetConnectionString("PdfGeneratorDb");
        _ = pdfGeneratorConnectionString ?? throw new RequiredConfigNotDefined("ConnectionStrings:PdfGeneratorDb");

        Builder.Services.AddHangfireServer();
        Builder.Services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(
            pgCfg => pgCfg.UseNpgsqlConnection(hangfireConnectionString)));

        Builder.Services.AddDbContext<PdfGeneratorDb>(optionsBuilder =>
            optionsBuilder.UseNpgsql(pdfGeneratorConnectionString));

        Builder.Services.RegisterPdfGenerator();
        Builder.Services.RegisterConversionService();
        Builder.Services.AddScoped<IPdfConversionRepository, PdfConversionRepository>();
        Builder.Services.RegisterMinioFileStorage(Builder.Configuration);
        Builder.Services.AddAutoMapper(typeof(PdfConversionProfile).Assembly);

        var app = Builder.Build();
        
        await EnsureDbCreated(hangfireConnectionString);

        Log.Information("Starting up {ServiceName} service", "Worker");
        
        await app.RunAsync();
    }

    private static async Task EnsureDbCreated(string connectionString)
    {
        var dbContextOptions = new DbContextOptionsBuilder()
            .UseNpgsql(connectionString)
            .Options;

        var dbContext = new DbContext(dbContextOptions);

        await dbContext.Database.EnsureCreatedAsync();
    }
}