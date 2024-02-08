using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.PdfConversions;
using PdfGenerator.Application.PdfConversions.Profiles;
using PdfGenerator.FileStorage.Extensions;
using PdfGenerator.Infrastructure.DataAccess;
using PdfGenerator.Infrastructure.Extensions;
using PdfGenerator.Observability.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.RegisterMinioFileStorage(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.RegisterConversionService();
builder.Services.RegisterPdfGenerator();
builder.Services.AddScoped<IPdfConversionRepository, PdfConversionRepository>();
builder.Services.AddDbContext<PdfGeneratorDb>(ob =>
    ob.UseNpgsql(builder.Configuration.GetConnectionString("PdfGeneratorDb")));
builder.Services.AddAutoMapper(typeof(PdfConversionProfile).Assembly);
builder.Services.RegisterMinioFileStorage(builder.Configuration);
builder.Services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(
    pgCfg => pgCfg.UseNpgsqlConnection(builder.Configuration.GetConnectionString("Hangfire"))));
builder.RegisterSerilogLogger();

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<PdfGeneratorDb>();
await db.Database.MigrateAsync();

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

app.MapControllers();

app.Run();