namespace Lu.Ma.Tests;

public class IntegrationTests
{
    [Fact]
    public async Task GetEventAsync_ShouldReturnEvents()
    {
        if (!TestHelpers.HasEnv())
        {
            return;
        }

        await foreach (var @event in TestHelpers.CalendarManager!.ListEventsAsync())
        {
            Assert.NotNull(@event);

            await foreach (var guest in TestHelpers.EventManager!.GetEventGuestsAsync(@event.ApiId!))
            {
                Assert.NotNull(guest);
            }
        }
    }
}