using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyMillennium.Contracts.Messages;
using MyMillennium.Functions.Services;

namespace MyMillennium.Functions;

public class ProcessArtImageFunction
{
    private readonly ILogger<ProcessArtImageFunction> _logger;
    private readonly ProcessArtImageService _processArtImageService;
    public ProcessArtImageFunction(ILogger<ProcessArtImageFunction> logger, ProcessArtImageService processArtImageService)
    {
        _logger = logger;
        _processArtImageService = processArtImageService;
    }

    [Function(nameof(ProcessArtImageFunction))]
    public async Task Run(
        [ServiceBusTrigger("process-art-image", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var processArtImage = message.Body.ToObjectFromJson<ProcessArtImage>();

        _logger.LogInformation($"Message retrieved from queue. Processing ArtItem: {processArtImage?.ArtItemId}, BlobName: {processArtImage?.BlobName}");

        //TODO: Add error handling if processArtImage is null
        await _processArtImageService.ProcessArtItemAsync(processArtImage);

        await messageActions.CompleteMessageAsync(message);
    }
}