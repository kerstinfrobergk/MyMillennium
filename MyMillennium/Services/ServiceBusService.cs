using Azure.Messaging.ServiceBus;

namespace MyMillenniumApi.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _sender;

        public ServiceBusService(ServiceBusClient serviceBusClient, IConfiguration config)
        {
            _serviceBusClient = serviceBusClient;
            var queueName = config["AzureServiceBus:QueueName"] ?? throw new InvalidOperationException("Azure Service Bus queue name could not be found.");

            _sender = _serviceBusClient.CreateSender(queueName);
        }

        public async Task SendProcessArtImageAsync(ProcessArtImage processArtImage)
        {
            var messageBody = BinaryData.FromObjectAsJson(processArtImage);

            var message = new ServiceBusMessage(messageBody)
            {
                ContentType = "application/json",
                Subject = nameof(ProcessArtImage),
            };

            await _sender.SendMessageAsync(message);
        }
    }
}
