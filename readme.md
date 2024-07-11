![PathCat Icon](https://raw.githubusercontent.com/Zettersten/Lu.Ma/main/icon.png)

# Lu.Ma ✨

[![NuGet Badge](https://buildstats.info/nuget/Lu.Ma)](https://www.nuget.org/packages/Lu.Ma/)

Welcome to the unofficial Lu.Ma .NET SDK! This SDK is designed to provide a seamless way to interact with the Lu.Ma API, allowing you to manage events and calendars programmatically. The SDK is available under the MIT license, and contributions are welcome.

## Overview

The Lu.Ma .NET SDK offers two primary classes that developers will interact with:

- `CalendarManager`
- `EventManager`

These classes provide methods to create, update, and manage events and calendars on Lu.Ma. 

## Getting Started

### Installation

To get started with the Lu.Ma .NET SDK, you can download the package from [NuGet](https://www.nuget.org/packages/Lu.Ma/).

```sh
dotnet add package Lu.Ma
```

### Setting Up Dependency Injection

To use the Lu.Ma SDK with dependency injection in your .NET application, you need to configure your services. Here’s how you can do it:

1. Add the necessary services to your `Startup.cs` or `Program.cs`.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddLuMa(options =>
    {
        options.ApiKey = "YOUR_API_KEY";
    });

    // Other service configurations...
}
```

You can find the `ServiceCollectionExtensions` class [here](path/to/ServiceCollectionExtenstions.cs).

## Usage

Below are examples of how to use the `CalendarManager` and `EventManager` classes.

### CalendarManager

#### List Events

```csharp
var calendarManager = serviceProvider.GetService<CalendarManager>();
var events = await calendarManager.ListEventsAsync(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
foreach (var eventItem in events)
{
    Console.WriteLine($"{eventItem.Name} - {eventItem.StartTime}");
}
```

### EventManager

#### Create Event

```csharp
var eventManager = serviceProvider.GetService<EventManager>();
var newEvent = new CreateEventRequest
{
    Name = "Sample Event",
    StartTime = DateTime.UtcNow.AddDays(7),
    EndTime = DateTime.UtcNow.AddDays(7).AddHours(1),
    Description = "This is a sample event created using Lu.Ma SDK."
};

var createdEvent = await eventManager.CreateEventAsync(newEvent);
Console.WriteLine($"Event Created: {createdEvent.Id}");
```

#### Get Event Details

```csharp
var eventId = "EVENT_ID";
var eventDetails = await eventManager.GetEventAsync(eventId);
Console.WriteLine($"Event Name: {eventDetails.Name}");
```

## API Documentation

The Lu.Ma API is a JSON-based RESTful API that allows you to read data and take actions on your Lu.Ma events and guests. For detailed API documentation, visit the [Lu.Ma API Docs](https://docs.lu.ma).

### Requirements

- To use the Lu.Ma API, you'll need to pay for Luma Plus. [See our pricing page](https://lu.ma/pricing).

### Authentication

To authenticate your requests, you'll need an API key. You can generate an API key on your [Lu.Ma dashboard](https://lu.ma/personal/settings/options).

Here is an example CURL request to check authentication:

```sh
curl -X GET https://api.lu.ma/public/v1/event/get \
     -H "x-luma-api-key: YOUR_API_KEY"
```

## License

This SDK is available under the [MIT License](LICENSE).

## Contributions

Pull requests and contributions are welcome. Please open an issue to discuss any changes before submitting a pull request.

## About

For more information about the business and its offerings, visit [Lu.Ma](https://lu.ma/).

---

Thank you for using the Lu.Ma .NET SDK. We look forward to your contributions and feedback!