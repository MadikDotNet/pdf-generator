using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PdfGenerator.Messaging.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task StartEventBusAsync(this IApplicationBuilder applicationBuilder)
    {
        var bus = applicationBuilder.ApplicationServices.GetRequiredService<IBusControl>();

        await bus.StartAsync(TimeSpan.FromSeconds(10));
    }
}