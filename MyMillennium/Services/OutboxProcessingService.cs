namespace MyMillenniumApi.Services
{
    public class OutboxProcessingService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IServiceBusService _serviceBusService;
        private readonly ILogger<OutboxProcessingService> _logger;

        public OutboxProcessingService(IServiceScopeFactory serviceScopeFactory, IServiceBusService serviceBusService, ILogger<OutboxProcessingService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _serviceBusService = serviceBusService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
