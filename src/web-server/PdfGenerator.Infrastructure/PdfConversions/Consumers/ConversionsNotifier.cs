using MassTransit;
using Microsoft.AspNetCore.SignalR;
using PdfGenerator.IntegrationEvents;

namespace PdfGenerator.Infrastructure.PdfConversions.Consumers;

public class ConversionsNotifier(IHubContext<PdfConversionHub> hubContext)
    : IConsumer<HtmlConverted>, IConsumer<NewConversionQueued>
{
    public Task Consume(ConsumeContext<HtmlConverted> context)
    {
        return hubContext.Clients.All.SendAsync(PdfConversionHub.ConversionsUpdatedMethodName);
    }

    public Task Consume(ConsumeContext<NewConversionQueued> context)
    {
        return hubContext.Clients.All.SendAsync(PdfConversionHub.ConversionsUpdatedMethodName);
    }
}