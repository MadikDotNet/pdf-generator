using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.PdfConversions;
using PdfGenerator.Application.PdfConversions.Models;
using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Infrastructure.DataAccess;

/// <inheritdoc/>
public class PdfConversionRepository(PdfGeneratorDb db, IMapper mapper) : IPdfConversionRepository
{
    /// <inheritdoc/>
    public Task AddAsync(PdfConversion pdfConversion)
    {
        db.PdfConversions.Add(pdfConversion);
        return db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task SaveSuccessResultAsync(Guid conversionId, string resultPath)
    {
        var conversion = await db.PdfConversions
            .FirstOrDefaultAsync(q => q.Id == conversionId);

        if (conversion is null)
            return;

        conversion.ResultPath = resultPath;
        conversion.Status = ConversionStatus.Processed;

        await db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public Task<List<PdfConversionModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return db.PdfConversions
            .ProjectTo<PdfConversionModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PdfConversionModel?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return db.PdfConversions
            .ProjectTo<PdfConversionModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }
}