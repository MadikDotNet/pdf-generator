using AutoMapper;
using PdfGenerator.Application.PdfConversions.Models;
using PdfGenerator.Domain.PdfConversions;

namespace PdfGenerator.Application.PdfConversions.Profiles;

/// <summary>
/// Configures mapping rules for PDF conversion related objects.
/// </summary>
public class PdfConversionProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfConversionProfile"/> class.
    /// </summary>
    public PdfConversionProfile()
    {
        CreateMap<PdfConversion, PdfConversionModel>();
    }
}