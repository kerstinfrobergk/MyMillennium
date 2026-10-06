using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyMillennium.Contracts.Messages;
using MyMillennium.Functions.Services;

namespace MyMillennium.Functions;

public class ProcessArtImageFunction
{
    private readonly ILogger<ProcessArtImageFunction> _logger;
    private readonly IProcessArtImageService _processArtImageService;
    public ProcessArtImageFunction(ILogger<ProcessArtImageFunction> logger, IProcessArtImageService processArtImageService)
    {
        _logger = logger;
        _processArtImageService = processArtImageService;
    }

    [Function(nameof(ProcessArtImageFunction))]
    public async Task Run(
        [ServiceBusTrigger("process-art-image", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var processArtImage = message.Body.ToObjectFromJson<ProcessArtImage>()
            ?? throw new InvalidOperationException(
                "Could not deserialize Service Bus message to ProcessArtImage.");

        await _processArtImageService.ProcessArtItemAsync(processArtImage, cancellationToken);        

        await messageActions.CompleteMessageAsync(message, cancellationToken);

        _logger.LogInformation("Service Bus message for ArtItem {ArtItemId} completed.",
            processArtImage.ArtItemId);
    }
}