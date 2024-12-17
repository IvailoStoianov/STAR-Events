using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STAREvents.Services.Data.Interfaces;
using static STAREvents.Common.EntityValidationConstants.NotificationsConstants;

public class NotificationBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationsService>();
                await notificationService.SendEventNotificationsAsync();
            }


            await Task.Delay(TimeSpan.FromMinutes(NotificationIntervalInMins), stoppingToken);
        }
    }
}