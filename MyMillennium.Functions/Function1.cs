using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyMillenniumApi;

namespace MyMillennium.Functions;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(Function1))]
    public async Task Run(
        [ServiceBusTrigger("process-art-image", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var processArtImage = message.Body.ToObjectFromJson<ProcessArtImage>();

        _logger.LogInformation($"Message retrieved from queue. Processing ArtItem: {processArtImage?.ArtItemId}, BlobName: {processArtImage?.BlobItemName}");

        await messageActions.CompleteMessageAsync(message);
    }
}