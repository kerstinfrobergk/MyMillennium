using Azure.Messaging.ServiceBus;

namespace MyMillenniumApi.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public ServiceBusService(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        // TODO: Add service to DI

    }
}
