using Azure.Messaging.ServiceBus;

namespace MyMillenniumApi.Services
{
    public interface IServiceBusService
    {
        Task SendMessageAsync(string payload, string messageType, CancellationToken cancellationToken);
    }

    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusSender _sender;

        public ServiceBusService(ServiceBusClient serviceBusClient, IConfiguration config)
        {
            var queueName = config["AzureServiceBus:QueueName"] ?? throw new InvalidOperationException("Azure Service Bus queue name could not be found.");

            _sender = serviceBusClient.CreateSender(queueName);
        }

        public async Task SendMessageAsync(string payload, string messageType, CancellationToken cancellationToken)
        {
            var sbMessage = new ServiceBusMessage(payload)
            {
                ContentType = "application/json",
                Subject = messageType
            };

            await _sender.SendMessageAsync(sbMessage, cancellationToken);
        }
    }
}
