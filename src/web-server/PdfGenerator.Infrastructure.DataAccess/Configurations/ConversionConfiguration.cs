using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Infrastructure.DataAccess.Configurations;

/// <summary>
/// Configures the entity properties and relationships for the PdfConversion entity.
/// </summary>
public class ConversionConfiguration : IEntityTypeConfiguration<PdfConversion>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PdfConversion> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedNever();
        builder.Property(q => q.OriginFilePath).IsRequired();
        builder.HasIndex(q => q.Status);
        builder.HasIndex(q => q.OriginFilePath).IsUnique();
        builder.HasIndex(q => q.ResultPath).IsUnique();
    }
}