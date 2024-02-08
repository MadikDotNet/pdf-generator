using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.PdfConversions;
using PdfGenerator.Application.PdfConversions.Profiles;
using PdfGenerator.FileStorage.Extensions;
using PdfGenerator.Infrastructure.DataAccess;
using PdfGenerator.Infrastructure.Extensions;
using PdfGenerator.Infrastructure.PdfConversions;
using PdfGenerator.Infrastructure.PdfConversions.Consumers;
using PdfGenerator.Messaging.Extensions;
using PdfGenerator.Observability.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.RegisterMinioFileStorage(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.RegisterConversionService();
builder.Services.RegisterPdfGenerator();
builder.Services.AddSignalR();
builder.Services.AddScoped<IPdfConversionRepository, PdfConversionRepository>();
builder.Services.AddDbContext<PdfGeneratorDb>(ob =>
    ob.UseNpgsql(builder.Configuration.GetConnectionString("PdfGeneratorDb")));
builder.Services.AddAutoMapper(typeof(PdfConversionProfile).Assembly);
builder.Services.RegisterMinioFileStorage(builder.Configuration);
builder.Services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(
    pgCfg => pgCfg.UseNpgsqlConnection(builder.Configuration.GetConnectionString("Hangfire"))));
builder.RegisterSerilogLogger();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConversionsNotifier>();
    
    x.UsingRabbitMq((hostContext, cfg) =>
    {
        cfg.SetHost(builder.Configuration);
        cfg.ConfigureEndpoints(hostContext);
    });
});

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<PdfGeneratorDb>();
await db.Database.MigrateAsync();

await app.StartEventBusAsync();

app.UseCors(
    options => options
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

app.UseHangfireDashboard();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHub<PdfConversionHub>(PdfConversionHub.HubRoute);

app.MapControllers();

app.Run();