﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using static STAREvents.Common.EntityValidationConstants.CleanUpConstants;

namespace STAREvents.Services.Data
{
    public class EventCleanupService : BackgroundService, IEventCleanupService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndDeleteExpiredEventsAsync();
                await Task.Delay(TimeSpan.FromMinutes(CleanupIntervalInMins), stoppingToken); 
            }
        }

        private async Task CheckAndDeleteExpiredEventsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventRepository = scope.ServiceProvider.GetRequiredService<IRepository<Event, object>>();
                var events = await eventRepository.GetAllAsync();

                var expiredEvents = events.Where(e => e.EndDate < DateTime.UtcNow && !e.isDeleted).ToList();

                foreach (var eventEntity in expiredEvents)
                {
                    eventEntity.isDeleted = true;
                    await eventRepository.UpdateAsync(eventEntity);
                }
            }
        }
    }
}
