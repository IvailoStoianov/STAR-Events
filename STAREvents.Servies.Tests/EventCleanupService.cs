using Microsoft.Extensions.DependencyInjection;
using Moq;

using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class EventCleanupServiceTests : IDisposable
    {
        private Mock<IServiceProvider> _serviceProviderMock;
        private Mock<IServiceScopeFactory> _serviceScopeFactoryMock;
        private Mock<IServiceScope> _serviceScopeMock;
        private Mock<IRepository<Event, object>> _eventRepositoryMock;
        private EventCleanupService _eventCleanupService;

        [SetUp]
        public void SetUp()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            _serviceScopeMock = new Mock<IServiceScope>();
            _eventRepositoryMock = new Mock<IRepository<Event, object>>();

            _serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(_serviceScopeFactoryMock.Object);

            _serviceScopeFactoryMock.Setup(x => x.CreateScope())
                .Returns(_serviceScopeMock.Object);

            _serviceScopeMock.Setup(x => x.ServiceProvider)
                .Returns(_serviceProviderMock.Object);

            _serviceProviderMock.Setup(x => x.GetService(typeof(IRepository<Event, object>)))
                .Returns(_eventRepositoryMock.Object);

            _eventCleanupService = new EventCleanupService(_serviceProviderMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _eventCleanupService?.Dispose();
        }

        [Test]
        public async Task CheckAndDeleteExpiredEventsAsync_ShouldDeleteExpiredEvents()
        {
            var events = new List<Event>
        {
            new Event { EventId = Guid.NewGuid(), EndDate = DateTime.UtcNow.AddHours(-2), isDeleted = false },
            new Event { EventId = Guid.NewGuid(), EndDate = DateTime.UtcNow.AddHours(2), isDeleted = false },
            new Event { EventId = Guid.NewGuid(), EndDate = DateTime.UtcNow.AddHours(-1), isDeleted = false }
        };

            _eventRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(events);

            _eventRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Event>()))
                .ReturnsAsync(true);
            await InvokePrivateMethodAsync(_eventCleanupService, "CheckAndDeleteExpiredEventsAsync");

            _eventRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Event>(e => e.isDeleted == true && e.EndDate < DateTime.UtcNow)), Times.Exactly(2));
            _eventRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Event>(e => e.EndDate > DateTime.UtcNow)), Times.Never);
        }

        [Test]
        public void ExecuteAsync_ShouldCancelWhenTokenIsRequested()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            Assert.DoesNotThrowAsync(() => _eventCleanupService.StartAsync(cancellationTokenSource.Token));
        }

        [Test]
        public async Task ExecuteAsync_ShouldInvokeCheckAndDeleteExpiredEventsPeriodically()
        {
   
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(200);

            _eventRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Event>());

            _eventRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Event>()))
                .ReturnsAsync(true);

            await _eventCleanupService.StartAsync(cancellationTokenSource.Token);

            _eventRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.AtLeastOnce);
        }

        private async Task InvokePrivateMethodAsync(object instance, string methodName, params object[] parameters)
        {
            var method = instance.GetType()
                .GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (method == null)
                throw new InvalidOperationException($"Method '{methodName}' not found in '{instance.GetType().Name}'.");

            var task = method.Invoke(instance, parameters) as Task;

            if (task != null)
            {
                await task.ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            _eventCleanupService?.Dispose();
        }
    }

}