using Microsoft.EntityFrameworkCore;
using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Infrastructure.DataAccess;

/// <summary>
/// Represents the database context for the PDF Generator application.
/// </summary>
public class PdfGeneratorDb(DbContextOptions<PdfGeneratorDb> options) : DbContext(options)
{
    /// <summary>
    /// Gets PdfConversions.
    /// </summary>
    public DbSet<PdfConversion> PdfConversions => Set<PdfConversion>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PdfGeneratorDb).Assembly);
    }
}