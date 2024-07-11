using Lu.Ma.Abstractions;
using Lu.Ma.Http;
using Lu.Ma.Models;
using Lu.Ma.Models.Shared;
using Moq;

namespace Lu.Ma.Tests;

public class CalendarManagerTests
{
    [Fact]
    public async Task ListEventsAsync_ShouldReturnEvents()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var mockResponse = new ListEventsResponse
        {
            Entries = [
                new CalendarEntry { ApiId = "event1" },
                new CalendarEntry { ApiId = "event2" }
            ],
            NextCursor = null
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<ListEventsResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var eventResults = new List<CalendarEntry>();
        await foreach (var events in calendarManager.ListEventsAsync())
        {
            eventResults.Add(events);
        }

        // Assert
        Assert.Equal(2, eventResults.Count);
        Assert.Equal("event1", eventResults[0].ApiId);
        Assert.Equal("event2", eventResults[1].ApiId);
    }

    [Fact]
    public async Task ImportPeopleAsync_ShouldCallImportPeopleEndpoint()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var request = new ImportPeopleRequest
        {
            Infos = [
                new ImportPeopleType { Email = "john@example.com" },
                new ImportPeopleType { Email = "jane@example.com" }
            ]
        };

        // Act
        await calendarManager.ImportPeopleAsync(request);

        // Assert
        mockHttpClient.Verify(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ListEventsAsync_ShouldReturnEventsWithinTimeRange()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var mockResponse = new ListEventsResponse
        {
            Entries = [
                new CalendarEntry { ApiId = "event2", Event = new() { StartAt = DateTime.Now.AddDays(1) } }
            ],
            NextCursor = null
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<ListEventsResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var eventResults = new List<CalendarEntry>();
        await foreach (var events in calendarManager.ListEventsAsync(after: DateTime.Now))
        {
            eventResults.Add(events);
        }

        // Assert
        Assert.Single(eventResults);
        Assert.Equal("event2", eventResults[0].ApiId);
    }

    [Fact]
    public async Task ListEventsAsync_ShouldHandlePagination()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var mockResponse1 = new ListEventsResponse
        {
            Entries = [
                new CalendarEntry { ApiId = "event1" },
                new CalendarEntry { ApiId = "event2" }
            ],
            NextCursor = "cursor1"
        };
        var mockResponse2 = new ListEventsResponse
        {
            Entries = [
                new CalendarEntry { ApiId = "event3" },
                new CalendarEntry { ApiId = "event4" }
            ],
            NextCursor = null
        };

        mockHttpClient.SetupSequence(c => c.SendRequestAsync<ListEventsResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse1)
            .ReturnsAsync(mockResponse2);

        // Act
        var eventResults = new List<CalendarEntry>();
        await foreach (var events in calendarManager.ListEventsAsync())
        {
            eventResults.Add(events);
        }

        // Assert
        Assert.Equal(4, eventResults.Count);
        Assert.Equal("event1", eventResults[0].ApiId);
        Assert.Equal("event2", eventResults[1].ApiId);
        Assert.Equal("event3", eventResults[2].ApiId);
        Assert.Equal("event4", eventResults[3].ApiId);
    }

    [Fact]
    public async Task ImportPeopleAsync_ShouldReturnWithoutException()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var request = new ImportPeopleRequest
        {
            Infos = [
                new ImportPeopleType { Email = "john@example.com" },
                new ImportPeopleType { Email = "jane@example.com" }
            ]
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK });

        // Act
        await calendarManager.ImportPeopleAsync(request);

        // Assert
        // No exception thrown
    }

    [Fact]
    public async Task ImportPeopleAsync_ShouldThrowExceptionWithInvalidRequest()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var request = new ImportPeopleRequest
        {
            Infos = []
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .Throws<HttpRequestException>();

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => calendarManager.ImportPeopleAsync(request));
    }

    [Fact]
    public async Task ListEventsAsync_ShouldReturnEmptyListWithNoCursorAndNoResults()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var calendarManager = new CalendarManager(mockHttpClient.Object);
        var mockResponse = new ListEventsResponse
        {
            Entries = null,
            NextCursor = null
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<ListEventsResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var eventResults = new List<CalendarEntry>();
        await foreach (var events in calendarManager.ListEventsAsync())
        {
            eventResults.Add(events);
        }

        // Assert
        Assert.Empty(eventResults);
    }
}