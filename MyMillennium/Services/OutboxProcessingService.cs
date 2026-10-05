using Microsoft.EntityFrameworkCore;
using MyMillennium.Data.DataAccess;

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

        private async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var scopedDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var outboxMessages = await scopedDbContext.OutboxMessages
                .Where(x => x.ProcessedAtUtc == null)
                .ToListAsync(cancellationToken);

            foreach (var outboxMessage in outboxMessages)
            {
                await _serviceBusService.SendMessageAsync(outboxMessage.Payload, outboxMessage.MessageType, cancellationToken);

                outboxMessage.ProcessedAtUtc = DateTime.UtcNow;

                await scopedDbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Processed outbox message at time: {ProcessedAtUtc}",
                    outboxMessage.ProcessedAtUtc);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await ProcessOutboxMessagesAsync(cancellationToken);

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                await ProcessOutboxMessagesAsync(cancellationToken);
            }
        }
    }
}
