using Lu.Ma.Abstractions;
using Lu.Ma.Http;
using Lu.Ma.Models;
using Lu.Ma.Models.Shared;
using Moq;

namespace Lu.Ma.Tests;

public class EventManagerTests
{
    [Fact]
    public async Task CreateEventAsync_ShouldReturnCreatedEvent()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new CreateEventRequest
        {
            Name = "Test Event",
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(2)
        };
        var mockResponse = new CreateEventResponse
        {
            ApiId = "event1"
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<CreateEventResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var response = await eventManager.CreateEventAsync(request);

        // Assert
        Assert.Equal("event1", response.ApiId);
    }

    [Fact]
    public async Task GetEventGuestsAsync_ShouldReturnEventGuests()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var mockResponse = new GetEventGuestResponse
        {
            Entries = [
                new EventEntry { ApiId = "guest1" },
                new EventEntry { ApiId = "guest2" }
            ],
            NextCursor = null
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<GetEventGuestResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var guests = new List<EventEntry>();
        await foreach (var item in eventManager.GetEventGuestsAsync("event1"))
        {
            guests.Add(item);
        }

        // Assert
        Assert.Equal(2, guests.Count);
        Assert.Equal("guest1", guests[0].ApiId);
        Assert.Equal("guest2", guests[1].ApiId);
    }

    [Fact]
    public async Task UpdateEventAsync_ShouldCallUpdateEventEndpoint()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new UpdateEventRequest
        {
            EventApiId = "event1",
            Name = "Updated Event"
        };

        // Act
        await eventManager.UpdateEventAsync(request);

        // Assert
        mockHttpClient.Verify(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateEventAsync_ShouldThrowExceptionWithInvalidRequest()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new CreateEventRequest
        {
            Name = null,
            StartAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(2)
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .Throws<HttpRequestException>();

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => eventManager.CreateEventAsync(request));
    }

    [Fact]
    public async Task GetEventGuestsAsync_ShouldReturnGuestsWithinFilterParameters()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var mockResponse = new GetEventGuestResponse
        {
            Entries = [
                new EventEntry { ApiId = "guest1", Guest = new() { ApprovalStatus = "GOING" }},
                new EventEntry { ApiId = "guest3", Guest = new() { ApprovalStatus = "DECLINED" } }
            ],
            NextCursor = null
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<GetEventGuestResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var guests = new List<EventEntry>();
        await foreach (var item in eventManager.GetEventGuestsAsync("event1", "GOING"))
        {
            guests.Add(item);
        }

        // Assert
        Assert.Equal(2, guests.Count);
        Assert.Equal("guest1", guests[0].ApiId);
        Assert.Equal("guest3", guests[1].ApiId);
    }

    [Fact]
    public async Task GetEventGuestsAsync_ShouldHandlePagination()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var mockResponse1 = new GetEventGuestResponse
        {
            Entries = [
                new EventEntry { ApiId = "guest1" },
                    new EventEntry { ApiId = "guest2" }
            ],
            NextCursor = "cursor1"
        };
        var mockResponse2 = new GetEventGuestResponse
        {
            Entries = [
                new EventEntry { ApiId = "guest3" },
                    new EventEntry { ApiId = "guest4" }
            ],
            NextCursor = null
        };

        mockHttpClient.SetupSequence(c => c.SendRequestAsync<GetEventGuestResponse>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse1)
            .ReturnsAsync(mockResponse2);

        // Act
        var guests = new List<EventEntry>();
        await foreach (var item in eventManager.GetEventGuestsAsync("event1"))
        {
            guests.Add(item);
        }

        // Assert
        Assert.Equal(4, guests.Count);
        Assert.Equal("guest1", guests[0].ApiId);
        Assert.Equal("guest2", guests[1].ApiId);
        Assert.Equal("guest3", guests[2].ApiId);
        Assert.Equal("guest4", guests[3].ApiId);
    }

    [Fact]
    public async Task UpdateEventAsync_ShouldThrowExceptionWithInvalidRequest()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new UpdateEventRequest
        {
            EventApiId = null,
            Name = "Updated Event"
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .Throws<HttpRequestException>();

        // Act and Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => eventManager.UpdateEventAsync(request));
    }

    [Fact]
    public async Task GetEventGuestAsync_ShouldReturnEventGuestByApiId()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var mockResponse = new EventEntry
        {
            ApiId = "guest1",
            Guest = new EventGuest { UserEmail = "john@example.com" }
        };

        mockHttpClient.Setup(c => c.SendRequestAsync<EventEntry>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockResponse);

        // Act
        var guest = await eventManager.GetEventGuestAsync("event1", "guest1");

        // Assert
        Assert.Equal("guest1", guest.ApiId);
        Assert.NotNull(guest.Guest);
        Assert.Equal("john@example.com", guest.Guest.UserEmail);
    }

    [Fact]
    public async Task AddGuestsAsync_ShouldCallAddGuestsEndpoint()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new AddGuestRequest
        {
            EventApiId = "event1",
            Guests = [
                new () { Email = "john@example.com" },
                new () { Email = "jane@example.com" }
            ]
        };

        // Act
        await eventManager.AddGuestsAsync(request);

        // Assert
        mockHttpClient.Verify(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateGuestStatusAsync_ShouldCallUpdateGuestStatusEndpoint()
    {
        // Arrange
        var mockHttpClient = new Mock<ILumaHttpClient>();
        var eventManager = new EventManager(mockHttpClient.Object);
        var request = new UpdateGuestStatusRequest
        {
            EventApiId = "event1",
            Guest = new EventGuestItem { Name = "guest1" },
            Status = "GOING"
        };

        // Act
        await eventManager.UpdateGuestStatusAsync(request);

        // Assert
        mockHttpClient.Verify(c => c.SendRequestAsync<object>(It.IsAny<Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}