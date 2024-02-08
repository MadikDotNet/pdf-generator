using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;

namespace PdfGenerator.Messaging.Extensions;

public static class DependencyInjectionExtensions
{
    public static void SetHost(this IRabbitMqBusFactoryConfigurator cfg, IConfiguration configuration)
    {
        cfg.Host($"{configuration["RabbitMQ:HostName"]}", configuration["RabbitMQ:VirtualHost"], h =>
        {
            h.Username(configuration["RabbitMQ:UserName"]);
            h.Password(configuration["RabbitMQ:Password"]);
        });
    }
}